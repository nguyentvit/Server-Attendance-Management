using AttendanceManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO
{
    public class ShiftDTO
    {
        [Required(ErrorMessage = "Shift Name can't be blank")]
        public ShiftOptions ShiftName { get; set; }
        [Required(ErrorMessage = "Time in can't be blank")]
        public string Time_In { get; set; } = string.Empty;
        [Required(ErrorMessage = "Time out can't be blank")]
        public string Time_Out { get; set; } = string.Empty;
        [Required(ErrorMessage = "DepartmentId can't be blank")]
        public Guid DepartmentId { get; set; }

    }
}
