using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RenoCare.Core.Middleware
{
    /// <summary>
    /// Represents an exception handling middleware.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoking the middleware action.
        /// </summary>
        /// <param name="context">Encapsulate all information for the current HTTP request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<string> { Succeded = false };

                switch (ex)
                {
                    case UnauthorizedAccessException:
                        responseModel.Message = "Unauthorized Access";
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ValidationException validationExcp:
                        responseModel.Message = validationExcp.Message ?? "One or more validation errors occurred.";
                        responseModel.Errors = validationExcp.Errors.Select(e => e.PropertyName + " : " + e.ErrorMessage).ToList();
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    case KeyNotFoundException:
                        responseModel.Message = "The requested item can not be found.";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case DbUpdateException:
                        responseModel.Message = "The requested item can not be updated.";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ArgumentNullException:
                        responseModel.Message = ex.Message ?? "The requested item can not be null.";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case SystemException:
                        responseModel.Message = ex.Message ?? "Internal Server Error.";
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case Exception:
                        responseModel.Message = ex.Message ?? "Bad request";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        responseModel.Message = "Unhandled Exception, please contact the application admin.";
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(responseModel));
            }
        }

        #endregion
    }
}
