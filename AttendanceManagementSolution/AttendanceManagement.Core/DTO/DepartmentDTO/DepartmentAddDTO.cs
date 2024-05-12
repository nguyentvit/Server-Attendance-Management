using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DepartmentDTO
{
    public class DepartmentAddDTO
    {
        [Required(ErrorMessage = "DepartmentName can't be blank")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
