using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DXUploadXAFBlazor.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;

namespace DXUploadXAFBlazor.Module.Blazor.Editors.New;

[PropertyEditor(typeof(FileData), "MultipleFilesData", false)]
public class MultipleUploadFilesPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
{
    private readonly Guid _guid = Guid.NewGuid();
    private IObjectSpace _objectSpace;

    public MultipleUploadFilesPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

    public override MultipleUploadFilesModel ComponentModel => (MultipleUploadFilesModel)base.ComponentModel;

    protected override IComponentModel CreateComponentModel()
    {
        var model = new MultipleUploadFilesModel();
        
        model.FilesUploading = EventCallback.Factory.Create<FilesUploadingEventArgs>(this, async args =>
        {
            foreach (var file in args.Files)
            {
                var fileSize = (int)file.Size;
                using var uploadStream = file.OpenReadStream(fileSize);
                try
                {
                    int totalBytesRead = 0;
                    int bytesReadCount = 0;
                    byte[] fileBytes = new byte[fileSize];
                    do
                    {
                        bytesReadCount = await uploadStream.ReadAsync(fileBytes, totalBytesRead, fileSize - totalBytesRead);
                        totalBytesRead += bytesReadCount;
                    } while (bytesReadCount != 0);

                    using var memoryStream = new MemoryStream(fileBytes);
                    var fileData = (FileData)_objectSpace.CreateObject(typeof(FileData));
                    fileData.LoadFromStream(file.Name, memoryStream);
                    var fileContent = _objectSpace.CreateObject<FileContent>();
                    fileContent.Content = fileData;
                    ((MultipleFiles)CurrentObject).Files.Add(fileContent);
                    _objectSpace.CommitChanges();
                }
                catch (Exception exception)
                {
                    var test = exception;
                }
                finally
                {
                    uploadStream?.Close();
                }
            }
        });
        return model;
    }

    public void Setup(IObjectSpace objectSpace, XafApplication application)
    {
        _objectSpace = objectSpace;
    }
}
