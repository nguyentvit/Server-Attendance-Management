using AttendanceManagement.Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using System;

namespace AttendanceManagement.Core.DTO.AttendanceDTO
{
    public class AttendanceResponseDTO
    {
        public Guid AttendanceId { get; set; }
        public DateTime Time { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PathImg { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(AttendanceResponseDTO))
            {
                return false;
            }

            AttendanceResponseDTO attendance_to_compare = (AttendanceResponseDTO)obj;
            return AttendanceId == attendance_to_compare.AttendanceId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class AttendanceExtensions
    {
        public static AttendanceResponseDTO ToAttendanceResponse(this Attendance attendance)
        {
            var user = attendance.User;
            var pathBase = "../Img/";
            return new AttendanceResponseDTO()
            {
                AttendanceId = attendance.AttendanceId,
                Time = attendance.Time,
                Status = attendance.Status,
                UserEmail = user.Email,
                UserId = user.Id,
                UserName = user.PersonName,
                PathImg = Path.Combine(pathBase, user.Id + "_" + attendance.PathImg + ".jpg")
            };
        }
    }
}
