using System;

namespace RenoCare.Core.Features.Reviews.Dtos
{
    public class ReviewDto
    {
        public string PatientName { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
