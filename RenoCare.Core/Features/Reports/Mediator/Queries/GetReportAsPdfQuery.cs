using IronPdf;
using IronPdf.Rendering;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RazorLight;
using RenoCare.Core.Base;
using RenoCare.Core.Conatracts.Files;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.DialysisUnits.Dtos;
using RenoCare.Core.Features.Reports.Dtos;
using RenoCare.Domain;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Features.Reports.Mediator.Queries
{
    public class GetReportAsPdfQueryRequest : IRequest<FileResponse>
    {
        public int ReportId { get; set; }
    }

    public class GetReportAsPdfQueryRequestHandler : ResponseHandler,
        IRequestHandler<GetReportAsPdfQueryRequest, FileResponse>
    {
        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webEnv;
        private readonly RazorLightEngine _razorLightEngine;

        public GetReportAsPdfQueryRequestHandler(IWebHostEnvironment webEnv, IMediator mediator, IRepository<DialysisUnit> unitRepo)
        {
            _webEnv = webEnv;
            _razorLightEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(_webEnv.WebRootPath) // Point to the root of your project
                .UseMemoryCachingProvider() // Use memory caching for compiled templates
                .EnableDebugMode() // Enable debug mode for detailed error information
                .Build();
            _mediator = mediator;
            _unitRepo = unitRepo;
        }

        public async Task<FileResponse> Handle(GetReportAsPdfQueryRequest request, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new GetReportQueryRequest { Id = request.ReportId });

            switch (result.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadHttpRequestException(result.Message);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException(result.Message);
                case HttpStatusCode.NotFound:
                    throw new FileNotFoundException(result.Message);
            }

            var report = result.Data;

            var unit = await _unitRepo.ApplyQueryAsync(async qry =>
                    await qry.Where(x => x.Id == report.DialysisUnitId).Select(x => new UnitSpecificationsDto
                    {
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
                    }).FirstOrDefaultAsync());


            var template_model = new ReportPDF
            {
                Report = report,
                PdfCreatedOn = DateTime.Now,
                DialysisUnit = unit
            };

            var dirPath = Path.Combine(_webEnv.ContentRootPath, "Views", "Templates", "Report");
            var templatePath = Path.Combine(dirPath, "report_pdf_template.cshtml");

            // Render the template using the relative path
            var html = await _razorLightEngine.CompileRenderAsync("C:/home/site/Views/Templates/Report/report_pdf_template.cshtml", template_model);

            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;
            renderer.RenderingOptions.ForcePaperSize = true;
            renderer.RenderingOptions.PaperSize = PdfPaperSize.A4;

            renderer.RenderingOptions.MarginTop = 0;
            renderer.RenderingOptions.MarginBottom = 0;
            renderer.RenderingOptions.MarginLeft = 0;
            renderer.RenderingOptions.MarginRight = 0;


            using var pdfDoc = renderer.RenderHtmlAsPdf(html, dirPath);

            return new FileResponse
            {
                File = pdfDoc.BinaryData,
                FileName = $"{template_model.PdfCreatedOn}-{template_model.Report.Patient.PatientName}.pdf"
            };
        }
    }
}
