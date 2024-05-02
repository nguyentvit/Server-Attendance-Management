using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class Attendance
    {
        [Key]
        public Guid AttendanceId { get; set; }
        [Required(ErrorMessage = "Time can't be blank")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Status can't be blank")]
        public bool Status { get; set; } // true: in, false: out
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

    }
}
