using DevExpress.ExpressApp.Blazor.Components.Models;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DXUploadXAFBlazor.Module.Blazor.Editors
{
    public class UploadFileModel : ComponentModelBase
    {
        internal Subject<Unit> UploadedSubject = new Subject<Unit>();
        public UploadFileModel(Guid guid)
        {
            Name = guid.ToString();
            UploadUrl = $"/api/Upload/UploadFile?Editor={Name}";
            AllowMultiFileUpload = true;
            DropZone = @"
<div id=""overviewDemoDropZone"" class=""card custom-drop-zone jumbotron custom-center"">
    <svg class=""drop-file-icon mb-3"" role=""img"" style=""width: 42px; height: 42px;""></svg>
          <span>Drag and Drop File Here</span>
</div>
";
        }

        public IObservable<Unit> Uploaded => UploadedSubject.AsObservable();

        public string Value
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public bool ReadOnly
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }

        public string UploadUrl
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public string DropZone
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public string Name
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public int ChunkSize
        {
            get => GetPropertyValue<int>();
            set => SetPropertyValue(value);
        }
        public bool AllowMultiFileUpload
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public bool AllowCancel
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public bool AllowPause
        {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
    }
}
