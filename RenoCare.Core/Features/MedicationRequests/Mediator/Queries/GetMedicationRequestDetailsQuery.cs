using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.MedicationRequests.DTOs;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.MedicationRequests.Mediator.Queries
{
    public class GetMedicationRequestDetailsQueryRequest : IRequest<ApiResponse<List<MedicationRequestDetailsDto>>>
    {
    }

    public class GetMedicationRequestDetailsQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetMedicationRequestDetailsQueryRequest, ApiResponse<List<MedicationRequestDetailsDto>>>
    {
        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IRepository<Patient> _patientRepo;

        public GetMedicationRequestDetailsQueryRequestHandler(IRepository<MedicationRequest> medReqRepo, IHttpContextAccessor ctxAccessor, IRepository<Patient> patientRepo)
        {
            _medReqRepo = medReqRepo;
            _ctxAccessor = ctxAccessor;
            _patientRepo = patientRepo;
        }

        public async Task<ApiResponse<List<MedicationRequestDetailsDto>>> Handle(GetMedicationRequestDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var patientId = await _patientRepo.Table.Where(x => x.UserId == curr_user).Select(x => x.Id).FirstOrDefaultAsync();

            if (patientId == null)
                return BadRequest<List<MedicationRequestDetailsDto>>();

            var list = await _medReqRepo.ApplyQueryAsync(async qry =>
            {
                return await qry.Where(x => x.PatientId == patientId)
                   .Select(x => new MedicationRequestDetailsDto
                   {
                       Id = x.Id,
                       UnitId = x.DialysisUnitId,
                       DialysisUnitName = x.DialysisUnit.Name,
                       FormattedAdress = x.DialysisUnit.Address + ", " + x.DialysisUnit.Country + ", " + x.DialysisUnit.City,
                       ReportId = x.ReportId,
                       Time = new DateTime(x.Session.Time.Ticks).ToString("hh:mm tt"),
                       Date = x.AppointmentDate.ToString("ddd, MMM dd, yyyy"),
                       Status = x.Status.Name
                   }).ToListAsync();
            });

            return Success(list);
        }
    }
}
