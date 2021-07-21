using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UploadFileBackend.Models.DTOs;

namespace UploadFileBackend.Core.Services
{
    public interface IFileService
    {
        public Task CreateAsync(IFormFile file);
        public Task<List<FileItem>> GetFileListAsync();
        public Task<MemoryStream> GetFileByNameAsync(string fileName);
    }
}