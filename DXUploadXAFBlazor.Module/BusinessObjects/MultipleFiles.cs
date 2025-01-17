using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace DXUploadXAFBlazor.Module.BusinessObjects;

[DefaultClassOptions]
public class MultipleFiles : BaseObject
{
    private FileData _uploadFile;
    [NonPersistent]
    [EditorAlias("MultipleFilesData")]
    public FileData UploadFile
    {
        get => _uploadFile;
        set => SetPropertyValue(nameof(UploadFile), ref _uploadFile, value);
    }

    [Association("MultipleFiles-Files"), Aggregated]
    public XPCollection<FileContent> Files => GetCollection<FileContent>(nameof(Files));

    public MultipleFiles(Session session) : base(session) { }
}
