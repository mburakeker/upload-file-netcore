using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UploadFileBackend.Models.AppSettings;

namespace UploadFileBackend.Api.Configuration
{
    public static class AppSettingsConfigurationExtension
    {
        public static IServiceCollection ConfigureEnvironmentVariables(this IServiceCollection services,IConfiguration configuration)
        {
            var minioSettings = new MinioSettings();
            configuration.Bind(nameof(MinioSettings), minioSettings);
            services.AddSingleton(minioSettings);
            return services;
        }
    }
}