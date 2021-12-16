using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DXUploadXAFBlazor.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.FileAttachments.Blazor.Components;
using DevExpress.ExpressApp.FileAttachment.Blazor.Editors;
using DevExpress.ExpressApp.FileAttachments.Blazor.Editors;
using DevExpress.ExpressApp.FileAttachment.Blazor.Editors.Adapters;
using System.Reflection;

namespace DXUploadXAFBlazor.Module.Blazor.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ViewController1 : ObjectViewController<DetailView, DomainObject1>
    {
        public ViewController1()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.CustomizeViewItemControl<FileDataPropertyEditor>(this, customizeActio);
        }

        private void customizeActio(FileDataPropertyEditor obj)
        {
           if(obj.Control is FileDataEditorComponentAdapter fileDataEditorComponentAdapter)
            {
                var type = fileDataEditorComponentAdapter.GetType();
                var createComponent = type.GetMethod("CreateComponent", BindingFlags.NonPublic | BindingFlags.Instance);
                var ttt = createComponent.GetType();
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
