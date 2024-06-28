using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reviews.Mediator.Commands
{
    /// <summary>
    /// Represents a request to add a review for a given dialysis unit
    /// </summary>
    public class CreateReviewCommandRequest : IRequest<ApiResponse<string>>
    {
        public int UnitId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
    }

    /// <summary>
    /// A validator for the create review request
    /// </summary>
    public class CreateReviewCommandRequestValidator : AbstractValidator<CreateReviewCommandRequest>
    {
        public CreateReviewCommandRequestValidator()
        {
            RuleFor(x => x.UnitId)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(x => x.Rating)
                .NotNullWithMessage().NotEmptyWithMessage()
                .InclusiveBetween(0, 5);
        }
    }

    /// <summary>
    /// Represents a handler for the request to create a review for a given dialysis unit
    /// </summary>
    public class CreateReviewCommandRequestHandler : ResponseHandler,
        IRequestHandler<CreateReviewCommandRequest, ApiResponse<string>>
    {
        #region Fields 

        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IRepository<Review> _reviewRepo;
        private readonly IRepository<MedicationRequest> _medReqsRepo;
        private readonly IRepository<Patient> _patientRepo;

        #endregion

        #region Ctor

        public CreateReviewCommandRequestHandler(IHttpContextAccessor ctxAccessor,
            IRepository<Review> reviewRepo,
            IRepository<MedicationRequest> medReqsRepo,
            IRepository<Patient> patientRepo)
        {
            _ctxAccessor = ctxAccessor;
            _reviewRepo = reviewRepo;
            _medReqsRepo = medReqsRepo;
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to create a review for a given dialysis unit
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the response.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(CreateReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()?.Value;

            var patientId = await _patientRepo.ApplyQueryAsync(async q =>
                await q.Where(x => x.UserId == curr_user).Select(x => x.Id).FirstOrDefaultAsync());

            var is_allowed = await _medReqsRepo.Table.AnyAsync(x => x.PatientId == patientId
                && x.DialysisUnitId == request.UnitId && x.StatusId == 3);

            if (!is_allowed)
                return BadRequest<string>();

            await _reviewRepo.InsertAsync(new Review
            {
                PatientId = patientId,
                DialysisUnitId = request.UnitId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreationDate = DateTime.Now
            });

            await _reviewRepo.SaveAsync();

            return Created(Boolean.TrueString);
        }

        #endregion
    }
}
