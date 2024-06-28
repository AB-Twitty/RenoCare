namespace RenoCare.Domain.MetaData
{
    /// <summary>
    /// Represents end points routing.
    /// </summary>
    public static class Router
    {
        public const string Root = "Api";
        public const string Version = "/V1";
        public const string Rule = Root + Version;

        /// <summary>
        /// Represents account controller end points routing.
        /// </summary>
        public static class AccountRouting
        {
            public const string Login = Rule + "/Login";
            public const string SendEmailConfirmation = Rule + "/EmailConfirmation/{userId}";
            public const string ConfirmEmail = Rule + "/ConfirmEmail/{userId}";
            public const string SendResetPasswordEmail = Rule + "/ResetPassword";
            public const string ResetPassword = Rule + "/ResetPassword/{userId}";
            public const string SetPasswordWithOtp = Rule + "/Password/Reset/OTP";
        }

        /// <summary>
        /// Represents healthcare provider methods end points routing.
        /// </summary>
        public static class HealthCareProviderRouting
        {
            public const string CreateHealthCareUser = Rule + "/HealthCare/Create";
            public const string NewCome = Rule + "/Unit/Newcome";
        }

        /// <summary>
        /// Represents dialysis unit methods end points routing.
        /// </summary>
        public static class DialysisUnitRouting
        {
            public const string Details = Rule + "/Dialysis-Unit/Details/{id}";
            public const string List = Rule + "/Dialysis-Unit/List";
            public const string ListForPatients = Rule + "/Dialysis-Units/List-For-Patients";
        }

        /// <summary>
        /// Represents patient controller end points routing.
        /// </summary>
        public static class PatientRouting
        {
            public const string List = Rule + "/Patients";
            public const string Medical = Rule + "/Patients/{id}";
        }

        /// <summary>
        /// Represents reviews controller end points routing.
        /// </summary>
        public static class ReviewRouting
        {
            public const string ListForUnit = Rule + "/Reviews";
            public const string Create = Rule + "/Review/Create";
        }

        /// <summary>
        /// Represents patients medical information controller end points routing.
        /// </summary>
        public static class PatientsMedicalInfo
        {
            public const string DiabetesTypes = Rule + "/Diabetes/Types";
            public const string HypertensionTypes = Rule + "/Hypertension/Types";
            public const string SmokingStatuses = Rule + "/Smoking/Status";
        }

        /// <summary>
        /// Represents medication requests controller end points routing.
        /// </summary>
        public static class MedicationRequestRouting
        {
            public const string List = Rule + "/Medication/Requests";
            public const string Details = Rule + "/Medication/Requests/{requestId}";
            public const string Status = Rule + "/Medication/Requests/Status";
            public const string Types = Rule + "/Medication/Requests/Types";
        }

        /// <summary>
        /// Represents ametities controller end points routing.
        /// </summary>
        public static class AmentitiesRouting
        {
            public const string List = Rule + "/Amentities";
        }

        /// <summary>
        /// Represents viruses controller end points routing.
        /// </summary>
        public static class VirusesRouting
        {
            public const string List = Rule + "/Viruses";
        }

        /// <summary>
        /// Represents reports controller end points routing.
        /// </summary>
        public static class ReportRouting
        {
            public const string GetById = Rule + "/Report";
            public const string Create = Rule + "/Report/Create";
        }
    }
}
