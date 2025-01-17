using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace DXUploadXAFBlazor.Module.BusinessObjects;

public class FileContent : BaseObject
{
    private MultipleFiles _multipleFiles;
    [Association("MultipleFiles-Files")]
    public MultipleFiles MultipleFiles
    {
        get => _multipleFiles;
        set => SetPropertyValue(nameof(MultipleFiles), ref _multipleFiles, value);
    }

    private FileData _content;
    public FileData Content
    {
        get => _content;
        set => SetPropertyValue(nameof(Content), ref _content, value);
    }

    public FileContent(Session session) : base(session) { }
}
