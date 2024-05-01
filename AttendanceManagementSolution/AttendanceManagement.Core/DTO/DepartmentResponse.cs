using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO
{
    public class DepartmentResponse
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set;} = string.Empty;
        public List<RegisterDTO> Users { get; set; } = new List<RegisterDTO>();
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(DepartmentResponse))
            {
                return false;
            }

            DepartmentResponse department_to_compare = (DepartmentResponse)obj;
            return DepartmentId == department_to_compare.DepartmentId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class DepartmentExtentions
    {
        public static DepartmentResponse ToDepartmentResponse(this Department department)
        {
            List<ApplicationUser> users = department.Users.ToList();
            List<RegisterDTO> userToRegisterDTO = users.Select(u => new RegisterDTO
            {
                PersonName = u.PersonName,
            }).ToList();
            return new DepartmentResponse()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Users = userToRegisterDTO
            };
        }
    }
}
