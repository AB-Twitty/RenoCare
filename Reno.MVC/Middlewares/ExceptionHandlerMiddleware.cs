using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Reno.MVC.Models;
using Reno.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RenoCare.MVC.Middlewares
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


                if (context.Response.StatusCode != (int)HttpStatusCode.OK)
                {
                    var error_model = GetErrorModel((HttpStatusCode)context.Response.StatusCode);


                    var viewResult = new ViewResult
                    {
                        ViewName = "Error",
                        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                        {
                            Model = error_model
                        }
                    };

                    var actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());
                    await viewResult.ExecuteResultAsync(actionContext);
                }

            }
            catch (Exception ex)
            {
                HttpStatusCode status;

                switch (ex)
                {
                    case ApiException ApiEx:
                        status = (HttpStatusCode)ApiEx.StatusCode;
                        break;
                    case UnauthorizedAccessException:
                        status = HttpStatusCode.Unauthorized;
                        break;
                    case FileNotFoundException or DirectoryNotFoundException or KeyNotFoundException or InvalidOperationException:
                        status = HttpStatusCode.NotFound;
                        break;
                    case BadHttpRequestException or InvalidOperationException or ArgumentNullException:
                        status = HttpStatusCode.BadRequest;
                        break;
                    default:
                        status = HttpStatusCode.InternalServerError;
                        break;
                }

                var error_model = GetErrorModel(status);


                var viewResult = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = error_model
                    }
                };

                var actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());
                await viewResult.ExecuteResultAsync(actionContext);
            }
        }

        private ErrorViewModel GetErrorModel(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.InternalServerError:
                    return new ErrorViewModel
                    {
                        Status = status,
                        ErrorName = HttpStatusCode.InternalServerError.ToString(),
                        Message = "The server encountered something unexpected that didn't allow it to complete the request. We apologize.",
                        ButtonCaption = "You can go back to main page:",
                        RedirectButton = "Dashboard",
                        RedirectUrl = "/"
                    };
                case HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden:
                    return new ErrorViewModel
                    {
                        Status = status,
                        ErrorName = HttpStatusCode.Unauthorized.ToString(),
                        Message = "Authentication is required to access the requested resource, You need to log in to access this resource.",
                        ButtonCaption = "Please authenticate yourself and try again.",
                        RedirectButton = "Login",
                        RedirectUrl = "/Login"
                    };
                case HttpStatusCode.BadRequest:
                    return new ErrorViewModel
                    {
                        Status = status,
                        ErrorName = HttpStatusCode.BadRequest.ToString(),
                        Message = "The server could not understand the request due to invalid syntax, Please check the syntax and try again."
                    };
                case HttpStatusCode.NotFound:
                    return new ErrorViewModel
                    {
                        Status = status,
                        ErrorName = HttpStatusCode.NotFound.ToString(),
                        Message = "Sorry, but the page you are looking for has note been found. Try checking the URL for error, then hit the refresh button on your browser or try found something else in our app."
                    };
                default:
                    return new ErrorViewModel
                    {
                        Status = status,
                        ErrorName = HttpStatusCode.BadRequest.ToString(),
                        Message = "The server could not understand the request due to invalid syntax, Please check the syntax and try again."
                    };
            }
        }

        #endregion
    }
}

