using RenoCare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class DialysisUnitDetailsDto : UnitSpecificationsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
        public int ReviewCnt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public IList<Amenity> Amenities { get; set; }
        public IList<SessionTimeDto> Sessions { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Virus> AcceptingViruses { get; set; }

        public IDictionary<string, IEnumerable<string>> GroupedSessions => Sessions
            .GroupBy(x => x.Day).ToDictionary(g => g.Key.ToString(), g => g.Select(x => x.FormattedTime));
    }
}
