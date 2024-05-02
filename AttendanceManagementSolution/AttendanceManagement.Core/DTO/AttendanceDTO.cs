using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO
{
    public class AttendanceDTO
    {
        public bool Status { get; set; }
        public Guid UserId { get; set; }
    }
}
