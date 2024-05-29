using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.SalaryDTO
{
    public class SalaryUpdateDTO
    {
        [Required(ErrorMessage = "Salary per hour can't be blank")]
        public double SalaryPerHour { get; set; }
    }
}
