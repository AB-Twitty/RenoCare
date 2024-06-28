using RenoCare.Domain;
using System;
using System.Collections.Generic;

namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class DialysisUnitDetailsDto : UnitSpecificationsDto
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public int ReviewCnt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public IList<Amenity> Amenities { get; set; }
        public IList<SessionTimeDto> Sessions { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Virus> AcceptingViruses { get; set; }
    }
}
