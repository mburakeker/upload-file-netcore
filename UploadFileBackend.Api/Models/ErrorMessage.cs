namespace UploadFileBackend.Api.Models
{
    public class FriendlyMessage
    {
        private string Message { get;}
        private string Detail { get;}
        private bool IsError { get; }

        public FriendlyMessage(string message, string detail, bool isError = false)
        {
            Message = message;
            Detail = detail;
            IsError = isError;
        }
    }
}