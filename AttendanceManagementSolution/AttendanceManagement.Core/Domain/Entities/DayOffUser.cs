using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class DayOffUser
    {
        public Guid DayOffId { get; set; }
        public DayOff DayOff { get; set; } = null!;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;

    }
}
