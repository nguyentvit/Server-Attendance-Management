using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class Shift
    {
        [Key]
        public Guid ShiftId { get; set; }
        [Required(ErrorMessage = "Shift Name can't be blank")]
        public string ShiftName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Time in can't be blank")]
        public TimeSpan Time_In { get; set; }
        [Required(ErrorMessage = "Time out can't be blank")]
        public TimeSpan Time_Out { get; set;}
        public Guid DepartmentId {  get; set; }
        public Department Department { get; set; } = null!;
        public List<ApplicationUser> Users { get; } = [];

    }
}
