using System.Collections.Generic;
using System.Net;

namespace RenoCare.Core.Base
{
    public class ApiResponse<T> where T : class
    {
        public ApiResponse()
        {

        }

        public ApiResponse(T data, string message = null)
        {
            Data = data;
            Message = message;
            Succeded = true;
        }

        public ApiResponse(string message, bool succeded = true)
        {
            Message = message;
            Succeded = succeded;
        }

        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public string Message { get; set; }
        public bool Succeded { get; set; }
        public T Data { get; set; }
        public IList<string> Errors { get; set; }
    }
}
