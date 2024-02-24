using System.Collections.Generic;
using System.Net;

namespace RenoCare.Core.Base
{
    public class ResponseHandler
    {
        public ApiResponse<T> Success<T>(T data, string message = null, object meta = null) where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.OK,
                Data = data,
                Message = message == null ? "Successfull Request" : message,
                Meta = meta,
                Succeded = true
            };
        }

        public ApiResponse<T> Created<T>(T data, object meta = null) where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.Created,
                Data = data,
                Message = "Created Successfully",
                Meta = meta,
                Succeded = true
            };
        }

        public ApiResponse<T> Deleted<T>() where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Deleted Successfully",
                Succeded = true
            };
        }

        public ApiResponse<T> Unauthorized<T>() where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized",
                Succeded = true
            };
        }

        public ApiResponse<T> NotFound<T>(string message = null) where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = message == null ? "Not Found" : message,
                Succeded = false
            };
        }

        public ApiResponse<T> UnprocessableEntity<T>(string message = null, IList<string> errors = null) where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Message = message == null ? "Unprocessable Entity" : message,
                Succeded = false,
                Errors = errors
            };
        }


        public ApiResponse<T> BadRequest<T>(string message = null) where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = message == null ? "Bad Request" : message,
                Succeded = false
            };
        }
    }
}
