using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Domain;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.DialysisUnits.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get the details of a given dialysis unit.
    /// </summary>
    public class GetDialysisUnitDetailsQueryRequest : IRequest<ApiResponse<DialysisUnitDetailsDto>>
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to get the details of a given dialysis unit.
    /// </summary>
    public class GetDialysisUnitDetailsQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetDialysisUnitDetailsQueryRequest, ApiResponse<DialysisUnitDetailsDto>>
    {
        #region Fields

        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region

        public GetDialysisUnitDetailsQueryRequestHandler(IRepository<DialysisUnit> unitRepo, IHttpContextAccessor ctxAccessor)
        {
            _unitRepo = unitRepo;
            _ctxAccessor = ctxAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get the details of a given dialysis unit.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the details of the given dialysis unit.
        /// </returns>
        public async Task<ApiResponse<DialysisUnitDetailsDto>> Handle(GetDialysisUnitDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (int.TryParse(_ctxAccessor.HttpContext.Items["unitId"].ToString(), out int unitId) && unitId != request.Id)
                {
                    return Unauthorized<DialysisUnitDetailsDto>();
                }
            }

            var result = await _unitRepo.ApplyQueryAsync(async qry =>
            {
                return await qry.Where(x => x.Id == request.Id)
                    .Include(x => x.Amenities).Include(x => x.Sessions)
                    .Include(x => x.Images).Include(x => x.Reviews).Include(x => x.AcceptingViruses)
                    .Select(x => new DialysisUnitDetailsDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Address = x.Address,
                        Country = x.Country,
                        City = x.City,
                        PhoneNumber = x.PhoneNumber,
                        IsHdSupported = x.IsHdSupported,
                        HdPrice = x.HdPrice,
                        IsHdfSupported = x.IsHdfSupported,
                        HdfPrice = x.HdPrice,

                        Images = x.Images.ToList(),
                        Amenities = x.Amenities.ToList(),
                        Sessions = x.Sessions.OrderBy(x => x.Day).ThenBy(x => x.Time)
                            .Select(x => new SessionTimeDto { Day = x.Day, Time = DateTime.Today.Add(x.Time) }).ToList(),

                        Rating = x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 0,
                        ReviewCnt = x.Reviews.Count(),

                        AcceptingViruses = x.AcceptingViruses.ToList(),

                        CreatedDate = x.CreationDate,
                        LastModifiedDate = x.LastModifiedDate,

                    })
                    .FirstOrDefaultAsync();

            });

            return Success(result);
        }

        #endregion
    }
}
