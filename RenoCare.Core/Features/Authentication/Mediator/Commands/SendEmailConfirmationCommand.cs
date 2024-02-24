using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Mail;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Domain.MetaData;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Authentication.Mediator.Commands
{
    /// <summary>
    /// Represents an email confirmation sending request with specidfied properties with a corresponding response.
    /// </summary>
    public class SendEmailConfirmationCommandRequest : IRequest<ApiResponse<string>>
    {
        /// <summary>
        /// Represents the user id that need to confirm his email.
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// Represents a handler for sending an email confirmation request.
    /// </summary>
    public class SendEmailConfirmationCommandHandler : ResponseHandler, IRequestHandler<SendEmailConfirmationCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IAuthService _authService;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        #endregion

        #region Ctor

        public SendEmailConfirmationCommandHandler(IAuthService authService, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _authService = authService;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles sending an email confirmation request.
        /// </summary>
        /// <param name="request">Sending email confirmation request.</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the authentication response.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(SendEmailConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            var confirmationToken = await _authService.GenerateEmailConfirmationTokenAsync(request.UserId);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            var callbackUrl = _urlHelper.Action(
                action: "ConfirmEmail", // action name
                controller: "Account", // controller name
                values: new { userId = request.UserId, code = encodedToken }, // route values
                protocol: "https", // protocol
                host: _httpContextAccessor.HttpContext.Request.Host.Value // host name
            );

            var pathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "Templates" + Path.DirectorySeparatorChar.ToString() + "Mail" + Path.DirectorySeparatorChar.ToString()
                + "Email_Template.cshtml";

            var subject = "Confirm Account Registration";
            var bodyHtml = "";

            using (StreamReader streamReader = File.OpenText(pathToTemplate))
            {
                bodyHtml = streamReader.ReadToEnd();
            }

            var user = await _authService.GetUserByIdAsync(request.UserId);

            //FullName : user full name
            //EmailHeader : email main header
            //EmailMessage : email body
            //CallbackUrl : callback url
            //ButtonLabel : text label for the link
            //CurrentYear : current year

            var emailHeader = "You're receiving this message because you recently signed up for a account.";
            var emailMessage = "Confirm your email address by clicking the button below. This step adds extra security to your business by verifying you own this email.";

            var emailValues = new
            {
                FullName = $"{user.FirstName} {user.LastName}",
                EmailHeader = emailHeader,
                EmailMessage = emailMessage,
                CallbackUrl = HtmlEncoder.Default.Encode(callbackUrl),
                ButtonLabel = "Confirm Email",
                CurrentYear = DateTime.Now.Year.ToString(),
            };

            var email = new Email
            {
                ToName = $"{user.FirstName} {user.LastName}",
                ToAddress = $"{user.Email}",
                Subject = subject,
                Body = bodyHtml
            };

            try
            {
                if (await _emailSender.SendEmailAsync(email, emailValues))
                    return Success(Boolean.TrueString, Transcriptor.Identity.ConfirmationEmailSent);
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
