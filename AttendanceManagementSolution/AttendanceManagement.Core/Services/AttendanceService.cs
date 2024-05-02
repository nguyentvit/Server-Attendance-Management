using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.ServiceContracts;
using System;

namespace AttendanceManagement.Core.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendancesRepository _attendancesRepository;
        public AttendanceService(IAttendancesRepository attendancesRepository)
        {
            _attendancesRepository = attendancesRepository;
        }

        public async Task<AttendanceResponse?> AddAttendance(Attendance attendance)
        {
            var attendanceAdder = await _attendancesRepository.AddAttendance(attendance);
            return attendanceAdder.ToAttendanceResponse();
        }

        public async Task<List<AttendanceResponse>> GetAllAttendances()
        {
            List<Attendance> attendances = await _attendancesRepository.GetAllAttendances();
            return attendances.Select(a => a.ToAttendanceResponse()).ToList();
        }

        public async Task<AttendanceResponse?> GetAttendance(Guid attendanceId)
        {
            var attendance = await _attendancesRepository.GetAttendance(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            return attendance.ToAttendanceResponse();
        }

        public async Task<List<AttendanceResponse>> GetAttendancesByDate(DateTime date)
        {
            var attendances = await _attendancesRepository.GetAttendancesByDate(date);
            return attendances.Select(a => a.ToAttendanceResponse()).ToList();
        }
    }
}
