﻿using Microsoft.AspNetCore.Identity;
using System;

namespace AttendanceManagement.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

    }
}
