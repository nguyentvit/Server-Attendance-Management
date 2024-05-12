using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.WorkingStatusDTO
{
    public class WorkingStatusReponseDTO
    {
        public string status { get; set; } = string.Empty;
        public Guid userId { get; set; }
        public DateTime time {  get; set; }

    }
    public static class WorkingStatusReponseDTOExtensions
    {
        public static WorkingStatusReponseDTO ToWorkingStatusReponseDTO(this Attendance attendance)
        {
            string status = (attendance.Status) ? "In" : "Out";
            return new WorkingStatusReponseDTO()
            {
                status = status,
                userId = attendance.UserId,
                time = attendance.Time,
            };
        }
    }
}
