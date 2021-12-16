using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DXUploadXAFBlazor.Module.BusinessObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DXUploadXAFBlazor.Module.Blazor.Editors
{
    [PropertyEditor(typeof(FileData), "FileData", false)]
    public class UploadFilePropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
    {
        readonly Guid _guid = Guid.NewGuid();
        private readonly UploadFileAdapter _uploadFileAdapter;

        public UploadFilePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
            _uploadFileAdapter = new UploadFileAdapter(new UploadFileModel(_guid));
        }

        protected override IComponentAdapter CreateComponentAdapter() => _uploadFileAdapter;

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            UploadFileMiddleware.FormFile.Where(t => t.editor == _guid.ToString())
                .Buffer(_uploadFileAdapter.ComponentModel.Uploaded)
                .SelectMany(list => list)
                .Do(formFile =>
                {
                    var fileData = (FileData)objectSpace.CreateObject(typeof(FileData));
                    fileData.LoadFromStream(formFile.name, new MemoryStream(formFile.bytes));
                    
                    ((DomainObject1)CurrentObject).Data = fileData;
                    ((DomainObject1)CurrentObject).FileData = fileData;
                    objectSpace.CommitChanges();

                })
                .Subscribe();
        }
    }

    public class UploadFileMiddleware
    {
        static readonly ISubject<(string name, byte[] bytes, string editor)> FormFileSubject = Subject.Synchronize(new Subject<(string name, byte[] bytes, string editor)>());

        public static IObservable<(string name, byte[] bytes, string editor)> FormFile => FormFileSubject.AsObservable();

        private readonly RequestDelegate _next;
        public UploadFileMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public static byte[] streamToByteArray(Stream input)
        {
            MemoryStream ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            string requestPath = context.Request.Path.Value.TrimStart('/');
            if (requestPath.StartsWith("api/Upload/UploadFile"))
            {
                var formFile = context.Request.Form.Files.First();
                FormFileSubject.OnNext((formFile.FileName, streamToByteArray(formFile.OpenReadStream()), context.Request.Query["Editor"]));
            }
            else
            {
                await _next(context);
            }
        }
    }
}

