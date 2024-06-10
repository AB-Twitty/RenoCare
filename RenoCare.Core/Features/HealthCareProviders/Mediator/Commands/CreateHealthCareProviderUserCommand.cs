using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Mail;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Domain.MetaData;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.HealthCareProviders.Mediator.Commands
{
    /// <summary>
    /// Represents a command to create an account for a new healthcare provider.
    /// </summary>
    public class CreateHealthCareProviderUserCommandRequest : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; }
    }

    /// <summary>
    /// A validator for the create healthcare provider command.
    /// </summary>
    public class CreateHealthCareProviderUserCommandValidator : AbstractValidator<CreateHealthCareProviderUserCommandRequest>
    {
        private readonly IAuthService _authService;

        public CreateHealthCareProviderUserCommandValidator(IAuthService authService)
        {
            _authService = authService;

            RuleFor(p => p.Email)
                .NotNullWithMessage().NotEmptyWithMessage()
                .EmailAddress();
            /*
                .MustAsync(async (email, _) =>
                {
                    return !await _authService.IsUserWithEmailExistsAsync(email);
                }).WithMessage("This E-mail Address Already Exists.");*/
        }
    }

    /// <summary>
    /// Represents a handler for the diabetes types query request.
    /// </summary>
    public class CreateHealthCareProviderUserCommandRequestHandler : ResponseHandler,
        IRequestHandler<CreateHealthCareProviderUserCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IAuthService _authService;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnv;

        #endregion

        #region Ctor

        public CreateHealthCareProviderUserCommandRequestHandler(IAuthService authService, IEmailSender emailSender, IWebHostEnvironment webHostEnv)
        {
            _authService = authService;
            _emailSender = emailSender;
            _webHostEnv = webHostEnv;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<string>> Handle(CreateHealthCareProviderUserCommandRequest request, CancellationToken cancellationToken)
        {
            var code = await _authService.CreateAccountWithOTPAsync(request.Email, "HealthCare");

            var pathToTemplate = _webHostEnv.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "Templates" + Path.DirectorySeparatorChar.ToString() + "Mail" + Path.DirectorySeparatorChar.ToString()
                + "First_Register_OTP_Email.cshtml";

            var subject = "Welcome To RenoCare Platform!";
            var bodyHtml = "";

            using (StreamReader streamReader = File.OpenText(pathToTemplate))
            {
                bodyHtml = streamReader.ReadToEnd();
            }

            var emailValues = new
            {
                Code = code,
                CurrentYear = DateTime.Now.Year.ToString()
            };

            var email = new Email
            {
                ToName = "",
                ToAddress = request.Email,
                Subject = subject,
                Body = bodyHtml
            };

            try
            {
                if (await _emailSender.SendEmailAsync(email, emailValues))
                    return Created(Boolean.TrueString);
                else
                    return Success(Boolean.FalseString, Transcriptor.Identity.EmailSendingFailure);
            }
            catch
            {
                return Success(Boolean.FalseString, Transcriptor.Identity.EmailSendingFailure);
            }

        }

        #endregion
    }
}
