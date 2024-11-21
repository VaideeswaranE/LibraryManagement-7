﻿
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        // Add other profile-related properties here
    }
}