using Microsoft.AspNetCore.StaticFiles;

namespace UploadFileBackend.Core.Helpers
{
    public static class MimeHelper
    {
        public static string GetMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }
    }
}