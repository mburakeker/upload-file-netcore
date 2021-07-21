using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace UploadFileBackend.Api.Models
{
    public class BaseResponse<T> where T: class
    {
        public FriendlyMessage FriendlyMessage { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int ItemCount => DataList.Any() ? DataList.Count : Convert.ToInt32(Data != null);
    }
}