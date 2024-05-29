using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class Salary
    {
        [Key]
        public Guid SalaryId { get; set; }
        [Required(ErrorMessage = "SalaryPerHour can't be blank")]
        public double SalaryPerHour { get; set; }
    }
}
