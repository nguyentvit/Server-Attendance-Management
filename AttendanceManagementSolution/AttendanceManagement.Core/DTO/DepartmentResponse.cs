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
        public List<UserReponse> Users { get; set; } = new List<UserReponse>();
        public List<ShiftResponse> Shifts { get; set; } = new List<ShiftResponse>();
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
            List<Shift> shifts = department.Shifts.ToList();

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
            List<ShiftResponse> shiftResponses = shifts.Select(s => s.ToShiftResponse()).ToList();
            return new DepartmentResponse()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Users = userReponses,
                Shifts = shiftResponses
            };
        }
    }
}
