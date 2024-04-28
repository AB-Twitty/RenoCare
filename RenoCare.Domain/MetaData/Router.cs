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
        }

        /// <summary>
        /// Represents patient controller end points routing.
        /// </summary>
        public static class PatientRouting
        {
            public const string List = Rule + "/Patients";
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
    }
}
