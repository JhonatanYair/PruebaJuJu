using System.Net;

namespace API.Common
{
    public class ResponseApi
    {
        public HttpStatusCode CodeHttp { get; set; }
        public object ObjectResponse { get; set; }
        public string Message { get; set; }
    }
}
