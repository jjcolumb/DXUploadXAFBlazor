using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace DXUploadXAFBlazor.Module.Blazor.Editors.New;

public class MultipleUploadFilesModel : ComponentModelBase
{
    /// <summary>
    /// Specifies MIME types users can add to the file list.
    /// </summary>
    public List<string> AcceptedFileTypes { get; set; } = new List<string>();

    /// <summary>
    /// Specifies whether users can cancel upload operations.
    /// </summary>
    public bool AllowCancel { get; set; } = true;

    /// <summary>
    /// Specifies file extensions that the File Input component can upload.
    /// </summary>
    public List<string> AllowedFileExtensions { get; set; } = new List<string>();

    /// <summary>
    /// Specifies whether users can add multiple files to the file list simultaneously.
    /// </summary>
    public bool AllowMultiFileUpload { get; set; } = true;

    /// <summary>
    /// Specifies the maximum number of files that users can add to the file list.
    /// </summary>
    public int MaxFileCount { get; set; } = 1000;

    /// <summary>
    /// Specifies the maximum file size in bytes.
    /// </summary>
    public int MaxFileSize { get; set; }
    
    /// <summary>
    /// Specifies the minimum file size in bytes.
    /// </summary>
    public int MinFileSize { get; set; }

    /// <summary>
    /// Specifies the caption of the built-in select button.
    /// </summary>
    public string SelectButtonText { get; set; }

    /// <summary>
    /// Specifies whether to show the file list.
    /// </summary>
    public bool ShowFileList { get; set; } = true;

    /// <summary>
    /// Specifies whether to show the built-in select button that invokes the Open dialog.
    /// </summary>
    public bool ShowSelectButton { get; set; } = true;

    /// <summary>
    /// Specifies when the File Input component starts upload operations.
    /// </summary>
    public UploadMode UploadMode { get; set; } = UploadMode.Instant;

    /// <summary>
    /// Allows you to upload selected files.
    /// </summary>
    public EventCallback<FilesUploadingEventArgs> FilesUploading { get; set; }
    
    public override Type ComponentType => typeof(DxFileInput);
}
