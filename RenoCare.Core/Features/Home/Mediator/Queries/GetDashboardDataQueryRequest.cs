using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Features.Home.Dtos;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Home.Mediator.Queries
{
    /// <summary>
    /// Represents a request to get the dashboard data
    /// </summary>
    public class GetDashboardDataQueryRequest : IRequest<ApiResponse<DashboardDto>>
    {
    }

    /// <summary>
    /// Represents a handler for the request to get the dashboard data
    /// </summary>
    public class GetDashboardDataQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetDashboardDataQueryRequest, ApiResponse<DashboardDto>>
    {
        #region Fields

        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IRepository<Report> _reportRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<MedicationRequest> _medReq;
        private readonly IRepository<Domain.MedicationRequestStatus> _medReqStatusRepo;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _ctxAccessor;

        #endregion

        #region Ctor

        public GetDashboardDataQueryRequestHandler(IRepository<DialysisUnit> unitRepo,
            IHttpContextAccessor ctxAccessor,
            IAuthService authService,
            IRepository<MedicationRequest> medReq,
            IRepository<Patient> patientRepo,
            IRepository<Domain.MedicationRequestStatus> medReqStatusRepo,
            IRepository<Report> reportRepo)
        {
            _unitRepo = unitRepo;
            _ctxAccessor = ctxAccessor;
            _authService = authService;
            _medReq = medReq;
            _patientRepo = patientRepo;
            _medReqStatusRepo = medReqStatusRepo;
            _reportRepo = reportRepo;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<DashboardDto>> Handle(GetDashboardDataQueryRequest request, CancellationToken cancellationToken)
        {
            DashboardDto dashboard = new DashboardDto
            {
                DialysisUnit = new Dictionary<string, Pair<int, int>>(),
                Patient = new Dictionary<string, Pair<int, int>>(),
                MedReq = new Dictionary<string, Pair<int, string>>(),
                Last7Months = new List<string>(),
                ReportsLast7Months = new List<int>(),
                PatientBirthDays = new List<string>(),
                UnitsTreatmentTypeCnt = new List<int>(),
            };

            DateTime currentDate = DateTime.Now;

            for (int i = 6; i >= 0; i--)
            {
                DateTime monthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i);
                DateTime monthEnd = monthStart.AddMonths(1);

                string monthName = currentDate.AddMonths(-i).ToString("MMMM", CultureInfo.InvariantCulture);
                dashboard.Last7Months.Add(monthName);
            }

            if (_ctxAccessor.HttpContext.User.IsInRole("HealthCare"))
            {
                if (!int.TryParse(_ctxAccessor.HttpContext.Items["unitId"]?.ToString(), out int unitId))
                    return BadRequest<DashboardDto>();

                var patientIds = await _medReq.Table.Where(x => x.DialysisUnitId == unitId && x.StatusId == 3)
                    .Select(x => x.PatientId).Distinct().ToListAsync();

                var patient_total = patientIds.Count;
                int patient_MOM = 9;
                dashboard.Patient.Add("Total", new Pair<int, int> { First = patient_total, Second = patient_MOM });

                var statuses = await _medReqStatusRepo.GetAllAsync();
                foreach (var status in statuses)
                {
                    dashboard.MedReq.Add(status.Name,
                        new Pair<int, string>
                        {
                            First = await _medReq.Table.Where(x => x.DialysisUnitId == unitId && x.StatusId == status.Id).CountAsync(),
                            Second = status.LabelClass
                        }
                    );
                }


                for (int i = 6; i >= 0; i--)
                {

                    DateTime monthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i);
                    DateTime monthEnd = monthStart.AddMonths(1);

                    int count = _reportRepo.Table.Count(r => r.DialysisUnitId == unitId && r.CreatedDate >= monthStart && r.CreatedDate < monthEnd);
                    dashboard.ReportsLast7Months.Add(count);
                }

                var patients = await _patientRepo.GetByIdsAsync(patientIds);

                dashboard.PatientBirthDays = patients.Select(x => x.BirthDate.ToString("yyyy-mm-dd")).ToList();
                dashboard.PatientGenderCnt = new Pair<int, int>
                {
                    First = patients.Where(x => x.Gender == Gender.Male).Count(),
                    Second = patients.Where(x => x.Gender == Gender.Female).Count()
                };

            }

            else if (_ctxAccessor.HttpContext.User.IsInRole("Admin"))
            {
                var curr_month = DateTime.Now.Month;
                var prev_month = DateTime.Now.AddMonths(-1).Month;


                //For Dialysis Units
                var total_units = _unitRepo.Table.Count();
                var total_curr_mon = _unitRepo.Table.Where(x => x.CreationDate.Month == curr_month).Count();
                var total_prev_mon = _unitRepo.Table.Where(x => x.CreationDate.Month == prev_month).Count();
                var MOM = (total_curr_mon - total_prev_mon) / (double)total_prev_mon;


                var total_healthcare = await _authService.CountInRole("HealthCare");
                var active_units = await _unitRepo.Table.CountAsync();
                var non_active_units = total_healthcare - active_units;

                int minus = RandomNumberGenerator.GetInt32(100);
                int active_MOM = 12;
                dashboard.DialysisUnit.Add("Active", new Pair<int, int> { First = active_units, Second = active_MOM });

                int non_active_MOM = -10;
                dashboard.DialysisUnit.Add("Non-Active", new Pair<int, int> { First = non_active_units, Second = non_active_MOM });


                dashboard.DialysisUnit.Add("Total", new Pair<int, int> { First = total_healthcare, Second = (int)(MOM * 100) });

                var patient_total = await _patientRepo.Table.CountAsync();
                int patient_MOM = 9;
                dashboard.Patient.Add("Total", new Pair<int, int> { First = patient_total, Second = patient_MOM });


                var statuses = await _medReqStatusRepo.GetAllAsync();
                foreach (var status in statuses)
                {
                    dashboard.MedReq.Add(status.Name,
                        new Pair<int, string>
                        {
                            First = await _medReq.Table.Where(x => x.StatusId == status.Id).CountAsync(),
                            Second = status.LabelClass
                        }
                    );
                }

                for (int i = 6; i >= 0; i--)
                {
                    DateTime monthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i);
                    DateTime monthEnd = monthStart.AddMonths(1);

                    int count = _reportRepo.Table.Count(r => r.CreatedDate >= monthStart && r.CreatedDate < monthEnd);
                    dashboard.ReportsLast7Months.Add(count);
                }

                dashboard.PatientBirthDays = await _patientRepo.Table.Select(x => x.BirthDate.ToString("yyyy-mm-dd")).ToListAsync();

                dashboard.PatientGenderCnt = new Pair<int, int>
                {
                    First = _patientRepo.Table.Where(x => x.Gender == Gender.Male).Count(),
                    Second = _patientRepo.Table.Where(x => x.Gender == Gender.Female).Count()
                };



                dashboard.UnitsTreatmentTypeCnt.Add(_unitRepo.Table.Count(x => x.IsHdSupported && !x.IsHdfSupported));
                dashboard.UnitsTreatmentTypeCnt.Add(_unitRepo.Table.Count(x => !x.IsHdSupported && x.IsHdfSupported));
                dashboard.UnitsTreatmentTypeCnt.Add(_unitRepo.Table.Count(x => x.IsHdSupported && x.IsHdfSupported));

            }

            return Success(dashboard);
        }

        #endregion
    }
}
