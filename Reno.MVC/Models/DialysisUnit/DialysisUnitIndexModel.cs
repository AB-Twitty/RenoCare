using Microsoft.AspNetCore.Http;
using Reno.MVC.Services.Base;
using System.Collections.Generic;

namespace Reno.MVC.Models.DialysisUnit
{
    public class DialysisUnitIndexModel
    {
        public UnitSpecificationsDto UnitSpec { get; set; }
        public IList<SessionTimeModel> Sessions { get; set; }
        public IList<IFormFile> Images { get; set; }
        public int Thumbnail { get; set; }
        public string Amenities { get; set; }
        public string Viruses { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }


        public IList<int> SelectedViruses { get; set; }
    }
}
