using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.ShiftDTO
{
    public class ShiftUpdateDTO
    {
        [Required(ErrorMessage = "Time in can't be blank")]
        public string Time_In { get; set; } = string.Empty;
        [Required(ErrorMessage = "Time out can't be blank")]
        public string Time_Out { get; set; } = string.Empty;
    }
}
