using System;

namespace UploadFileBackend.Models.DTOs
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public uint Size { get; set; }
        public string LastModifiedDate { get; set; }
    }
}