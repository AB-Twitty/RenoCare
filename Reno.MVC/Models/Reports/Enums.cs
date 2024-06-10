using System.ComponentModel.DataAnnotations;

namespace Reno.MVC.Models.Reports
{
    public enum VascularType
    {
        Fistula,
        Graft,
        Catheter
    }

    public enum DialyzerType
    {
        [Display(Name = "High-Flux")]
        HighFlux,
        [Display(Name = "Low-Flux")]
        LowFlux
    }
}
