using Reno.MVC.Services.Base;
using System.Collections.Generic;

namespace Reno.MVC.Models.DialysisUnit
{
    public class AllMedStatusModel
    {
        public IList<MedicationRequestStatus> MedReqStatus { get; set; }
    }
}
