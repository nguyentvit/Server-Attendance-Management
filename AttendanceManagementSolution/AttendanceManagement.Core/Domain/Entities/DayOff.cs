using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class DayOff
    {
        public Guid DayOffId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DayOffUser> DayOffUsers { get; } = new List<DayOffUser>();
    }
}
