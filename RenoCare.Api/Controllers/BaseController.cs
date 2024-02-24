using Microsoft.AspNetCore.Mvc;
using RenoCare.Core.Base;
using System.Net;

namespace RenoCare.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ObjectResult ApiResult<T>(ApiResponse<T> response) where T : class
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
    }
}
