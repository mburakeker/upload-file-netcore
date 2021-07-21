using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.Exceptions;
using UploadFileBackend.Models.AppSettings;
using UploadFileBackend.Models.DTOs;

namespace UploadFileBackend.Core.Services
{
    public class MinioService: IFileService
    {
        private readonly MinioSettings _minioSettings;
        private readonly ILogger<MinioService> _logger;
        public MinioService( ILogger<MinioService> logger, MinioSettings minioSettings)
        {
            _logger = logger;
            _minioSettings = minioSettings;
        }

        public async Task CreateAsync(IFormFile file)
        {
            try
            {
                var client = new MinioClient(_minioSettings.Endpoint, _minioSettings.AccessKey,
                    _minioSettings.SecretKey);
                if (!await client.BucketExistsAsync(_minioSettings.BucketName))
                    await client.MakeBucketAsync(_minioSettings.BucketName);
                var fileName = $"{DateTime.Now:yyyyMdd-HHmmss}-{file.FileName}";
                var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                await client.PutObjectAsync(_minioSettings.BucketName, fileName, stream, stream.Length,
                    file.ContentType);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception caught in MinioService.Create");
            }
        }

        public async Task<List<FileItem>> GetFileListAsync()
        {
            try
            {
                var client = new MinioClient(_minioSettings.Endpoint, _minioSettings.AccessKey,
                    _minioSettings.SecretKey);
                var items = client.ListObjectsAsync(_minioSettings.BucketName);
                var fileList = new List<FileItem>();
                await items.ForEachAsync(item => fileList.Add(item.Adapt<FileItem>()));
                return fileList;
            }
            catch (MinioException mex)
            {
                _logger.LogError(mex,"MinioException caught in GetFileListAsync");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Exception caught in GetFileListAsync");
                return null;
            }
            
        }

        public async Task<MemoryStream> GetFileByNameAsync(string fileName)
        {
            
            try
            {
                var client = new MinioClient(_minioSettings.Endpoint, _minioSettings.AccessKey,
                    _minioSettings.SecretKey);
               
                await client.StatObjectAsync(_minioSettings.BucketName, fileName);
                var stream = new MemoryStream();
                await client.GetObjectAsync(_minioSettings.BucketName, fileName,
                    (objectStream) => { objectStream.CopyTo(stream); });
                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            }
            catch (MinioException mex)
            {
                _logger.LogError(mex,"MinioException caught in GetFileByNameAsync");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Exception caught in GetFileByNameAsync");
                return null;
            }
        }
    }
}