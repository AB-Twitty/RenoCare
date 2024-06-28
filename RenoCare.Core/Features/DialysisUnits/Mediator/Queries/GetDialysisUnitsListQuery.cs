using MediatR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Extensions;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Core.Helpers;
using RenoCare.Core.Helpers.Contracts;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.DialysisUnits.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get the dialyis unit list 
    /// </summary>
    public class GetDialysisUnitsListQueryRequest : IRequest<ApiResponse<IPagedList<DialysisUnitListItemDto>>>,
        ISearchable
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;

        public string SortColumn { get; set; } = "UnitId";
        public string SortDirection { get; set; } = "ASC";

        public IDictionary<string, string> SearchDict { get; set; }
    }

    /// <summary>
    /// Represents a handler for the request to get a paged list of dialysis units.
    /// </summary>
    public class GetDialysisUnitsListQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetDialysisUnitsListQueryRequest, ApiResponse<IPagedList<DialysisUnitListItemDto>>>
    {
        #region Fields 

        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IRepository<Domain.MedicationRequestStatus> _medStatusRepo;
        private readonly IRepository<MedicationRequest> _medReqRepo;

        #endregion

        #region Ctor

        public GetDialysisUnitsListQueryRequestHandler(IRepository<DialysisUnit> unitRepo,
            IRepository<Domain.MedicationRequestStatus> medStatusRepo,
            IRepository<MedicationRequest> medReqRepo)
        {
            _unitRepo = unitRepo;
            _medStatusRepo = medStatusRepo;
            _medReqRepo = medReqRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request to get a paged list of dialysis units.
        /// </summary>
        /// <param name="request">Mediator request</param>
        /// <param name="cancellationToken">Represents a notification to cancel the request.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a paged list of dialysis units.
        /// </returns>
        public async Task<ApiResponse<IPagedList<DialysisUnitListItemDto>>> Handle(GetDialysisUnitsListQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _unitRepo.ApplyQueryAsync(async query =>
            {
                var statuses = await _medStatusRepo.GetAllAsync(q => q.OrderBy(s => s.Id));

                var statuses_names = statuses.Select(x => x.Name);

                var totalCount = query.Count(x => !x.IsDeleted);

                query = query
                    .Include(x => x.Amenities).Include(x => x.AcceptingViruses).Include(x => x.Reviews);

                if (request.SearchDict.ContainsKey("Amenities"))
                {
                    var filterAmenities = request.SearchDict["Amenities"].Split(',').Select(a => int.Parse(a.Trim())).ToList();
                    var amenitiesFilter = string.Join(" || ", filterAmenities.Select(fa => $"Amenities.Any(Id == {fa})"));

                    query = query.Where(amenitiesFilter);
                    request.SearchDict.Remove("Amenities");
                }

                if (request.SearchDict.ContainsKey("AcceptingViruses"))
                {
                    var filterViruses = request.SearchDict["AcceptingViruses"].Split(',').Select(a => int.Parse(a.Trim())).ToList();
                    var virusesFilter = string.Join(" || ", filterViruses.Select(fa => $"AcceptingViruses.Any(Id == {fa})"));

                    query = query.Where(virusesFilter);
                    request.SearchDict.Remove("AcceptingViruses");
                }

                var qry = query
                    .Select(x => new DialysisUnitListItemDto
                    {
                        UnitId = x.Id.ToString(),
                        Name = x.Name,
                        HealthCareProviderName = x.User.FirstName + " " + x.User.LastName,
                        Address = x.Address,
                        Country = x.Country,
                        City = x.City,
                        ContactNumber = x.PhoneNumber,
                        IsHdSupported = x.IsHdSupported,
                        HDTreatment = x.IsHdSupported ? $"{x.HdPrice}$" : "",
                        IsHdfSupported = x.IsHdfSupported,
                        HDFTreatment = x.IsHdfSupported ? $"{x.HdfPrice}$" : "",
                        Rating = x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 0,
                        Amenities = string.Join(", ", x.Amenities.OrderBy(a => a.Id).Select(a => a.Name)),
                        AcceptingViruses = string.Join(", ", x.AcceptingViruses.OrderBy(v => v.Id).Select(a => a.Abbreviation)),
                        CreationDate = DateTime.Now,
                    });

                qry = qry.FilterQuery(request);

                var sorting_by_medStatus = statuses_names.Contains(request.SortColumn);

                if (!sorting_by_medStatus && !string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortDirection))
                    qry = qry.OrderBy($"{request.SortColumn} {request.SortDirection}");

                var paged_list = await qry.ToPagedListAsync(request.PageIndex, request.PageSize, totalCount, 1, qry.Count());

                foreach (var unit in paged_list.Items)
                {
                    unit.MedReqCnts = await _medReqRepo.ApplyQueryAsync(async q =>
                    {
                        var medReqs = await q
                            .Where(m => m.DialysisUnitId == int.Parse(unit.UnitId))
                            .ToListAsync();

                        return medReqs
                            .GroupBy(m => m.Status.Name)
                            .Where(g => statuses_names.Contains(g.Key))
                            .ToDictionary(g => g.Key, g => g.Count());
                    });
                }


                if (sorting_by_medStatus)
                {
                    string sortExpression = $"MedReqCnts.Where(Key == \"{request.SortColumn}\").Select(Value).DefaultIfEmpty(0).FirstOrDefault() {request.SortDirection}";

                    paged_list.Items = paged_list.Items.AsQueryable().OrderBy(sortExpression).ToList();
                }

                return paged_list;
            });

            return Success(list);
        }
    }

    #endregion
}
