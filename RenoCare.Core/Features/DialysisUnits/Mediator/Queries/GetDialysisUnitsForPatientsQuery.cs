using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Core.Helpers;
using RenoCare.Domain;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.DialysisUnits.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get a paged list of dialysis units for patients
    /// </summary>
    public class GetDialysisUnitsForPatientsQueryRequest : IRequest<ApiResponse<IPagedList<DialysisUnitSimpleListItemDto>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Treatment { get; set; }
        public string Viruses { get; set; }
        public string Amenities { get; set; }
        public string Search { get; set; }
        public string Day { get; set; }
        public string SortBy { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to get a paged list of dialysis units for patients 
    /// </summary>
    public class GetDialysisUnitsForPatientsQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetDialysisUnitsForPatientsQueryRequest, ApiResponse<IPagedList<DialysisUnitSimpleListItemDto>>>
    {
        #region Fields

        private readonly IRepository<DialysisUnit> _unitRepo;

        #endregion

        #region Ctor

        public GetDialysisUnitsForPatientsQueryRequestHandler(IRepository<DialysisUnit> unitRepo)
        {
            _unitRepo = unitRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get a paged list of dialysis units for patients.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a paged list of dialysis units.
        /// </returns>
        public async Task<ApiResponse<IPagedList<DialysisUnitSimpleListItemDto>>> Handle(GetDialysisUnitsForPatientsQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _unitRepo.ApplyQueryAsync(async qry =>
            {
                qry = qry.Where(x => !x.IsDeleted);

                var totalcount = qry.Count();

                try
                {
                    if (!string.IsNullOrEmpty(request.Amenities))
                    {
                        var filterAmenities = request.Amenities.Split(',').Select(a => int.Parse(a.Trim())).ToList();
                        var amenitiesFilter = string.Join(" || ", filterAmenities.Select(fa => $"Amenities.Any(Id == {fa})"));

                        qry = qry.Where(amenitiesFilter);
                    }
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(request.Viruses))
                    {
                        var filterViruses = request.Viruses.Split(',').Select(a => int.Parse(a.Trim())).ToList();
                        var virusesFilter = string.Join(" || ", filterViruses.Select(fa => $"AcceptingViruses.Any(Id == {fa})"));

                        qry = qry.Where(virusesFilter);
                    }
                }
                catch { }

                if (!string.IsNullOrWhiteSpace(request.Treatment))
                {
                    var treatment = request.Treatment.Trim().ToLower();

                    if (treatment == "hd")
                        qry = qry.Where(x => x.IsHdSupported);

                    else if (treatment == "hdf")
                        qry = qry.Where(x => x.IsHdfSupported);
                }

                try
                {
                    if (!string.IsNullOrEmpty(request.Day))
                    {
                        string formattedDay = char.ToUpper(request.Day.Trim()[0]) + request.Day.Trim().Substring(1).ToLower();

                        if (Enum.TryParse(typeof(DayOfWeek), formattedDay, out var day))
                            qry = qry.Include(x => x.Sessions).Where(x => x.Sessions.Any(s => s.Day == (DayOfWeek)day));
                    }
                }
                catch { }


                if (!string.IsNullOrEmpty(request.Search))
                {
                    qry = qry.Where(x => x.Name.Contains(request.Search) || x.Address.Contains(request.Search)
                        || x.Country.Contains(request.Search) || x.City.Contains(request.Search));
                }

                var query = qry.Include(x => x.Images).Include(x => x.Reviews)
                        .Select(x => new DialysisUnitSimpleListItemDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Address = x.Address,
                            Country = x.Country,
                            City = x.City,
                            ContactNumber = x.PhoneNumber,
                            IsHdSupported = x.IsHdSupported,
                            HdPrice = x.HdPrice,
                            IsHdfSupported = x.IsHdfSupported,
                            HdfPrice = x.HdfPrice,

                            Rating = x.Reviews.Any() ? Math.Round(x.Reviews.Average(r => r.Rating) * 2, MidpointRounding.AwayFromZero) / 2 : 0,
                            ReviewsCnt = x.Reviews.Count,

                            ThumbnailImage = x.Images.Where(i => i.IsThumbnail).Select(i => i.Path).FirstOrDefault(),

                        });


                var sort = request.SortBy.Trim().ToLower();

                switch (sort)
                {
                    case "name":
                        query = query.OrderBy(x => x.Name); break;
                    case "rating":
                        query = query.OrderByDescending(x => x.Rating); break;

                    default:
                        query = query.OrderBy(x => x.Id); break;
                }

                return await query.ToPagedListAsync(request.PageIndex, request.PageSize, totalcount, 1, query.Count());

            });

            return Success(list);
        }

        #endregion
    }
}
