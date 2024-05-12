using AttendanceManagement.Core.Domain.Entities;
using System;

namespace AttendanceManagement.Core.DTO.DayOffDTO
{
    public class DayOffResponseAdminDTO
    {
        public Guid userId { get; set; }
        public string userName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string departmentName {  get; set; } = string.Empty;
    }
    public static class DayOffResponseAdminDTOExtensions
    {
        public static DayOffResponseAdminDTO ToDayOffResponseAdminDTO(this DayOffUser dayOffUser)
        {
            return new DayOffResponseAdminDTO()
            {
                Date = dayOffUser.DayOff.Date,
                Status = dayOffUser.Status,
                Reason = dayOffUser.Reason,
                userId = dayOffUser.UserId,
                userName = dayOffUser.User.PersonName,
                departmentName = dayOffUser.User.Department.DepartmentName,
            };
        }
    }
}
