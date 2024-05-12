using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name can't be blank")]
        public string DepartmentName { get; set; } = string.Empty;
        public ICollection<ApplicationUser> Users { get; } = new List<ApplicationUser>();
    }
}
