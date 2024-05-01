using AttendanceManagement.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace AttendanceManagement.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public Guid? DeparmentId {  get; set; }
        public Department? Department { get; set; }

    }
}
