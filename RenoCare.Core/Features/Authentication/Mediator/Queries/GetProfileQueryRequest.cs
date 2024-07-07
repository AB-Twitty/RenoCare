using MediatR;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Authentication.Contracts.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Mediator.Queries
{
    public class GetProfileQueryRequest : IRequest<ApiResponse<UserInfo>>
    {
        public string Id { get; set; }
    }

    public class GetProfileQueryHandler : ResponseHandler, IRequestHandler<GetProfileQueryRequest, ApiResponse<UserInfo>>
    {
        private readonly IAuthService _authService;

        public GetProfileQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<UserInfo>> Handle(GetProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _authService.GetUserByIdAsync(request.Id);

            var user_info = new UserInfo
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IsDeleted = user.IsDeleted,
            };

            return Success(user_info);
        }
    }
}
