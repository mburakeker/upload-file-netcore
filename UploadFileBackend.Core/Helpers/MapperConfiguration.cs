using Mapster;
using UploadFileBackend.Models.DTOs;

namespace UploadFileBackend.Core.Helpers
{
    public static class MapperConfiguration
    {
        public static void ConfigureMapper()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
            TypeAdapterConfig<Minio.DataModel.Item, FileItem>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Key)
                .Map(dest => dest.Type, src => MimeHelper.GetMimeTypeForFileExtension(src.Key))
                .Map(dest => dest.LastModifiedDate, src => src.LastModified);

        }
    }
}