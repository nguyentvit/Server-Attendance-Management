using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DepartmentDTO
{
    public class DepartmentUpdateDTO
    {
        [Required(ErrorMessage = "Department Name can't be blank")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
