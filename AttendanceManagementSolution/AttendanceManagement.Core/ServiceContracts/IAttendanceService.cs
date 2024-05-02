using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IAttendanceService
    {
        Task<List<AttendanceResponse>> GetAllAttendances();
        Task<List<AttendanceResponse>> GetAttendancesByDate(DateTime date);
        Task<AttendanceResponse?> GetAttendance(Guid attendanceId);
        Task<AttendanceResponse?> AddAttendance(Attendance attendance);
    }
}
