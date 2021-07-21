namespace UploadFileBackend.Models.AppSettings
{
    public class MinioSettings
    {
        public string BucketName { get; set; }
        public string Endpoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}