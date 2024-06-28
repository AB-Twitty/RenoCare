using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using RenoCare.Core.Base;
using System.Net;
using System.Threading.Tasks;

namespace RenoCare.Core.Middleware
{
    public class AuthorizationHandlerMiddleware : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged || authorizeResult.Forbidden)
            {
                var response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Unauthorized Access"
                };

                // Set the status code and content type
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                // Write the apiResponse as JSON in the response body
                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }

}
