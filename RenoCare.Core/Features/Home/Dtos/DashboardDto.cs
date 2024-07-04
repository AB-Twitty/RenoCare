using System.Collections.Generic;

namespace RenoCare.Core.Features.Home.Dtos
{
    public class DashboardDto
    {
        public IDictionary<string, Pair<int, int>> DialysisUnit { get; set; }
        public IDictionary<string, Pair<int, int>> Patient { get; set; }
        public Pair<int, int> PatientGenderCnt { get; set; }
        public IDictionary<string, Pair<int, string>> MedReq { get; set; }
        public IList<string> Last7Months { get; set; }
        public IList<int> ReportsLast7Months { get; set; }
        public IList<string> PatientBirthDays { get; set; }
        public IList<int> UnitsTreatmentTypeCnt { get; set; }
    }

    public class Pair<T, G>
    {
        public T First { get; set; }
        public G Second { get; set; }
    }
}
