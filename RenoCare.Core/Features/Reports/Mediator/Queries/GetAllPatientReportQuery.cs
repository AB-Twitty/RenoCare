using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Files;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Patients.DTOs;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Queries
{
    public class GetAllPatientReportQueryRequest : IRequest<FileResponse>
    {
    }

    public class GetAllPatientReportQueryHandler : ResponseHandler,
        IRequestHandler<GetAllPatientReportQueryRequest, FileResponse>
    {
        private readonly IRepository<Report> _reportRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly IMediator _mediator;

        public GetAllPatientReportQueryHandler(IRepository<Report> reportRepo, IHttpContextAccessor ctxAccessor, IRepository<Patient> patientRepo, IMediator mediator)
        {
            _reportRepo = reportRepo;
            _ctxAccessor = ctxAccessor;
            _patientRepo = patientRepo;
            _mediator = mediator;
        }

        public async Task<FileResponse> Handle(GetAllPatientReportQueryRequest request, CancellationToken cancellationToken)
        {
            var curr_user = _ctxAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


            var patientId = await _patientRepo.Table.Where(x => x.UserId == curr_user).Select(x => x.Id).FirstOrDefaultAsync();
            var name = await _patientRepo.Table.Where(x => x.UserId == curr_user).Select(x => x.User.FirstName + " " + x.User.LastName).FirstOrDefaultAsync();


            var reports = (await _mediator.Send(new GetReportsListQueryRequest
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                SortColumn = "SessionDate",
                SortDirection = "ASC",
                SearchDict = new Dictionary<string, string>() { { "Patient.Id", patientId.ToString() } }
            })).Data.Items;

            MemoryStream result = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(result))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reports");

                // Exclude properties to ignore
                List<string> excludedReportProperties = new List<string>
                {
                    "MedReqId", "DialysisUnitId", "CreatedDate", "LastModifiedDate", "Patient", "FormattedTime",
                };

                List<string> excludedPatientProperties = new List<string>
                {
                    "Id", "PatientName"
                };

                // Get properties to include
                var reportProperties = typeof(ReportDto).GetProperties()
                    .Where(p => !excludedReportProperties.Contains(p.Name))
                    .ToList();

                var patientProperties = typeof(PatientDto).GetProperties()
                    .Where(p => !excludedPatientProperties.Contains(p.Name))
                    .ToList();

                // Add headers for PatientDto
                worksheet.Cells[1, 1].Value = "Report Id";
                worksheet.Cells[1, 2].Value = "Patient_Name";


                for (int i = 0; i < patientProperties.Count; i++)
                {
                    worksheet.Cells[1, i + 3].Value = "Patient_" + patientProperties[i].Name;
                }

                // Add headers for ReportDto
                for (int i = 1; i < reportProperties.Count; i++)
                {
                    worksheet.Cells[1, patientProperties.Count + i + 2].Value = reportProperties[i].Name;
                }

                // Add data rows
                for (int row = 0; row < reports.Count; row++)
                {
                    int col = 3;
                    worksheet.Cells[row + 2, 2].Value = name;

                    foreach (var prop in patientProperties)
                    {
                        var value = prop.GetValue(reports[row].Patient);
                        if (prop.Name == "BirthDate")
                            value = ((DateTime)value).ToString("dd-MMM-yyyy");

                        worksheet.Cells[row + 2, col].Value = value;
                        col++;
                    }


                    foreach (var prop in reportProperties)
                    {
                        var value = prop.GetValue(reports[row]);
                        if (prop.Name == "Id")
                        {
                            worksheet.Cells[row + 2, 1].Value = value;
                            continue;
                        }
                        else if (prop.Name == "SessionDate")
                            value = ((DateTime)value).ToString("dd-MMM-yyyy");
                        else if (prop.Name == "SessionTime")
                            value = ((DateTime)value).ToString("hh:mm tt");

                        worksheet.Cells[row + 2, col].Value = value;
                        col++;
                    }


                }

                // Save the package
                package.Save();
            }

            // Reset the position of the stream before returning it
            result.Position = 0;
            return new FileResponse
            {
                File = result.ToArray(),
                FileName = $"{DateTime.Now}-{name}-Reports"
            };

        }
    }
}
