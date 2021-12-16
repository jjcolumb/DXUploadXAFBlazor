using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXUploadXAFBlazor.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DomainObject1 : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DomainObject1(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        FileData fileData;
        FileData data;
       
        [VisibleInListView(false)]
        [EditorAlias("FileData")]
        [DevExpress.Xpo.DisplayName("Upload Data")]
        public FileData Data
        {
            get => data;
            set => SetPropertyValue(nameof(Data), ref data, value);
        }
        [VisibleInListView(true)]
        [VisibleInDetailView(true)]
        [ModelDefault("AllowEdit", "False")]
        [DevExpress.Xpo.DisplayName("Uploaded Data")]
        public FileData FileData
        {
            get => fileData;
            set => SetPropertyValue(nameof(FileData), ref fileData, value);
        }
    }
}