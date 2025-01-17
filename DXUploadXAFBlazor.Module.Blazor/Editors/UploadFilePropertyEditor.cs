using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DXUploadXAFBlazor.Module.Blazor.Middlewares;
using DXUploadXAFBlazor.Module.BusinessObjects;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

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
}
