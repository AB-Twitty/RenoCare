using System;

namespace RenoCare.Core.Helpers
{
    public static class AgeFormatter
    {
        public static string CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            var months = today.Month - birthDate.Month;
            if (months < 0)
            {
                months += 12;
                age--;
            }

            return $"{age} Y {months} M";
        }
    }
}
