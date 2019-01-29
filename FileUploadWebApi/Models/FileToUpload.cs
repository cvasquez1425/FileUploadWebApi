using System;

public class FileToUpload
{
    public string FileName { get; set; }
    public string FileSize { get; set; }
    public string FileType { get; set; }
    public long LastModifiedTime { get; set; }
    public DateTime LastModifiedDate { get; set; }
    //Additional parameters to hold the file contents as a Base64 encoded string.
    public string FileAsBase64  { get; set; }
    public byte[] FileAsByteArray { get; set; }
}
