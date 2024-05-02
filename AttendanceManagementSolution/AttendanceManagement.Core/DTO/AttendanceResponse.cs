﻿using AttendanceManagement.Core.Domain.Entities;
using System;

namespace AttendanceManagement.Core.DTO
{
    public class AttendanceResponse
    {
        public Guid AttendanceId { get; set; }
        public DateTime Time {  get; set; }
        public bool Status { get; set; }
        public Guid UserId {  get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(AttendanceResponse))
            {
                return false;
            }

            AttendanceResponse attendance_to_compare = (AttendanceResponse)obj;
            return AttendanceId == attendance_to_compare.AttendanceId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class AttendanceExtensions
    {
        public static AttendanceResponse ToAttendanceResponse(this Attendance attendance)
        {
            var user = attendance.User;
            return new AttendanceResponse()
            {
                AttendanceId = attendance.AttendanceId,
                Time = attendance.Time,
                Status = attendance.Status,
                UserEmail = user.Email,
                UserId = user.Id,
                UserName = user.PersonName
            };
        }
    }
}