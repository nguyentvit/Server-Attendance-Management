using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.UserDTO;
using AttendanceManagement.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DepartmentDTO
{
    public class DepartmentResponseDTO
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public List<UserReponse> Users { get; set; } = new List<UserReponse>();
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(DepartmentResponseDTO))
            {
                return false;
            }

            DepartmentResponseDTO department_to_compare = (DepartmentResponseDTO)obj;
            return DepartmentId == department_to_compare.DepartmentId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class DepartmentExtentions
    {
        public static DepartmentResponseDTO ToDepartmentResponse(this Department department)
        {
            List<ApplicationUser> users = department.Users.ToList();

            List<UserReponse> userReponses = users.Select(u => new UserReponse
            {
                PersonName = u.PersonName,
                Email = u.Email,
                Gender = u.Gender,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber,
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Id = u.Id
            }).ToList();
            return new DepartmentResponseDTO()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Users = userReponses,
            };
        }
    }
}
