﻿namespace RenoCare.Core.Features.Authentication.Contracts.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
    }
}