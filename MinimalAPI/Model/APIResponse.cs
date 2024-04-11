using System.Net;

namespace MinimalAPI.Model
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }

        public object Data { get; set; }

        public List<string> ErrorMessages { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
    }
}
