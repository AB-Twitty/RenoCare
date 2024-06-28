using Reno.MVC.Services.Base;
using System;
using System.Collections.Generic;

namespace Reno.MVC.Models.Reports
{
    public class ReportIndexVM
    {
        public IList<(DateTime, int)> PrevReports { get; set; }

        public MedicationRequestListItemDto MedReq;

        public ReportDto Report { get; set; }

        public string ViewMode { get; set; }

    }
}
