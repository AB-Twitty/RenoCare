using System;
using System.Collections.Generic;

namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class DialysisUnitListItemDto
    {
        public string UnitId { get; set; }
        public string Name { get; set; }
        public string HealthCareProviderName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public double Rating { get; set; }
        public bool IsHdSupported { get; set; }
        public string HDTreatment { get; set; }
        public bool IsHdfSupported { get; set; }
        public string HDFTreatment { get; set; }
        public string Amenities { get; set; }
        public string AcceptingViruses { get; set; }

        public DateTime CreationDate { get; set; }

        public IDictionary<string, int> MedReqCnts { get; set; }
    }
}
