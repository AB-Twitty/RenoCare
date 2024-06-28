using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Files;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Core.Features.DialysisUnits.Validators;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.DialysisUnits.Mediator.Commands
{
    public class CreateNewcomeDialysisUnitCommandRequest : IRequest<ApiResponse<string>>
    {
        public UnitSpecificationsDto UnitSpecificationsDto { get; set; }
        public IList<SessionTimeDto> Sessions { get; set; }
        public IList<int> Amenities { get; set; }
        public IList<int> Viruses { get; set; }
        public IList<ImageUploadDto> Images { get; set; }
        public int ThumnailIdx { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }

    public class CreateNewcomeDialysisUnitCommandValidator :
        AbstractValidator<CreateNewcomeDialysisUnitCommandRequest>
    {
        public CreateNewcomeDialysisUnitCommandValidator(
            UnitSpecificationsValidator unitSpecValidator, SessionTimeValidator sessionValidator)
        {
            RuleFor(p => p.UnitSpecificationsDto)
                .NotNull()
                .SetValidator(unitSpecValidator);

            RuleFor(p => p.Images).Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(imgs => imgs.Count > 0).WithMessage("At least one image must be provided")
                .Must((model, imgs) => model.ThumnailIdx >= 0 && model.ThumnailIdx <= imgs.Count)
                .WithMessage("One image must be chosen as thumbnail")
                .Must(imgs => imgs.Count < 6).WithMessage("You can upload up to '6' images");

            RuleForEach(p => p.Sessions)
                .SetValidator(sessionValidator);

            RuleFor(p => p.Sessions)
                .Must(sessions => sessions == null || sessions
                    .GroupBy(s =>
                    {
                        var t = new TimeSpan(s.Time.Ticks);
                        return new { s.Day, t };
                    })
                    .All(g => g.Count() == 1))
                .WithMessage((model, sessions) =>
                {
                    var duplicateSessions = sessions
                        .GroupBy(s => new { s.Day, Time = new TimeSpan(s.Time.Ticks) })
                        .Where(g => g.Count() > 1)
                        .Select(g => $"{g.Key.Day}");
                    var duplicateSessionText = string.Join(",", duplicateSessions);
                    return $"Multiple sessions with the same time.#{duplicateSessionText}";
                })
                .WithName("multiSessions");


            RuleFor(p => p.FirstName)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.LastName)
                .NotNullWithMessage().NotEmptyWithMessage();

            RuleFor(p => p.Phone)
                .NotNullWithMessage().NotEmptyWithMessage();
        }
    }

    public class CreateNewcomeDialysisUnitCommandRequestHandler : ResponseHandler,
        IRequestHandler<CreateNewcomeDialysisUnitCommandRequest, ApiResponse<string>>
    {
        #region Fields

        private readonly IAuthService _authService;
        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IRepository<SessionTimetable> _sessionRepo;
        private readonly IRepository<Amenity> _amenitiesRepo;
        private readonly IRepository<Virus> _virusRepo;
        private readonly IRepository<Image> _imgsRepo;
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IFileUpload _fileUpload;

        #endregion

        #region Ctor

        public CreateNewcomeDialysisUnitCommandRequestHandler(IAuthService authService,
            IRepository<SessionTimetable> sessionRepo,
            IRepository<DialysisUnit> unitRepo,
            IHttpContextAccessor ctxAccessor,
            IRepository<Amenity> amenitiesRepo,
            IFileUpload fileUpload,
            IRepository<Image> imgsRepo,
            IRepository<Virus> virusRepo)
        {
            _authService = authService;
            _sessionRepo = sessionRepo;
            _unitRepo = unitRepo;
            _ctxAccessor = ctxAccessor;
            _amenitiesRepo = amenitiesRepo;
            _fileUpload = fileUpload;
            _imgsRepo = imgsRepo;
            _virusRepo = virusRepo;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<string>> Handle(CreateNewcomeDialysisUnitCommandRequest request, CancellationToken cancellationToken)
        {
            var c = FileDir.Images.ToString();
            //get current user id
            var curr_user = _ctxAccessor.HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            //check if it already has it's dialysis unit spec provided
            if (await _unitRepo.Table.AnyAsync(x => x.UserId == curr_user))
                return BadRequest<string>("Unit Specifications is already provided.");

            //update user info
            var user = await _authService.GetUserByIdAsync(curr_user);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.Phone;

            await _authService.UpdateUserInfoAsync(user);

            //create an entry in the dialysis unit table
            request.UnitSpecificationsDto.HdPrice =
                request.UnitSpecificationsDto.IsHdSupported ? request.UnitSpecificationsDto.HdPrice : null;

            request.UnitSpecificationsDto.HdfPrice =
                request.UnitSpecificationsDto.IsHdfSupported ? request.UnitSpecificationsDto.HdfPrice : null;

            var unit = new DialysisUnit
            {
                UserId = curr_user,
                Name = request.UnitSpecificationsDto.Name,
                Description = request.UnitSpecificationsDto.Description,
                Address = request.UnitSpecificationsDto.Address,
                City = request.UnitSpecificationsDto.City,
                Country = request.UnitSpecificationsDto.Country,
                PhoneNumber = request.UnitSpecificationsDto.PhoneNumber,
                IsHdSupported = request.UnitSpecificationsDto.IsHdSupported,
                HdPrice = request.UnitSpecificationsDto.HdPrice,
                IsHdfSupported = request.UnitSpecificationsDto.IsHdfSupported,
                HdfPrice = request.UnitSpecificationsDto.HdfPrice,

                Amenities = await _amenitiesRepo.GetByIdsAsync(request.Amenities),
                AcceptingViruses = await _virusRepo.GetByIdsAsync(request.Viruses),

                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };

            await _unitRepo.InsertAsync(unit);
            await _unitRepo.SaveAsync();


            //insert the available sessions
            foreach (var session in request.Sessions)
            {
                await _sessionRepo.InsertAsync(new SessionTimetable
                {
                    DialysisUnitId = unit.Id,
                    Time = new TimeSpan(session.Time.Ticks),
                    Day = session.Day
                });
            }

            await _sessionRepo.SaveAsync();


            // try upload images
            foreach (var img in request.Images)
            {
                var path = await _fileUpload.UploadFileAsync(img.Bytes, img.FileName, FileDir.Images);
                await _imgsRepo.InsertAsync(new Image
                {
                    DialysisUnitId = unit.Id,
                    Path = path,
                    Name = img.FileName,
                    IsThumbnail = request.Images.IndexOf(img) == request.ThumnailIdx
                });
            }
            await _imgsRepo.SaveAsync();

            return Success(Boolean.TrueString);
        }

        #endregion
    }
}
