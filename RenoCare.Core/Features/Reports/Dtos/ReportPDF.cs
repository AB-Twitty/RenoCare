using RenoCare.Core.Features.DialysisUnits.Dtos;
using System;

namespace RenoCare.Core.Features.Reports.Dtos
{
    public class ReportPDF
    {
        public ReportDto Report { get; set; }
        public DateTime PdfCreatedOn { get; set; }

        public UnitSpecificationsDto DialysisUnit { get; set; }
    }
}
