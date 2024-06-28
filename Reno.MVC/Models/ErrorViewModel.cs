using System.Net;

namespace Reno.MVC.Models
{
    public class ErrorViewModel
    {
        public HttpStatusCode Status { get; set; }
        public string ErrorName { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
        public string ButtonCaption { get; set; }
        public string RedirectButton { get; set; }
    }
}
