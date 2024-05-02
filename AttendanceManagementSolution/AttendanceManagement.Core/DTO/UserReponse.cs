using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO
{
    public class UserReponse
    {
        public Guid Id { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty ;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public Guid DepartmentId { get; set; } = Guid.Empty;
        public string DepartmentName { get ; set; } = string.Empty;
    }
}
