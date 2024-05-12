using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.AttendanceDTO;
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

        public async Task<AttendanceResponseDTO> AddAttendance(AttendanceAddDTO attendanceAddDTO)
        {
            Attendance attendance = new Attendance()
            {
                Status = attendanceAddDTO.Status,
                UserId = attendanceAddDTO.UserId,
                Time = DateTime.UtcNow.ToLocalTime(),
            };
            var attendanceAdder = await _attendancesRepository.AddAttendance(attendance);
            return attendanceAdder.ToAttendanceResponse();
        }

        public async Task<List<AttendanceResponseDTO>> GetAllAttendances()
        {
            List<Attendance> attendances = await _attendancesRepository.GetAllAttendances();
            return attendances.Select(a => a.ToAttendanceResponse()).ToList();
        }

        public async Task<AttendanceResponseDTO?> GetAttendance(Guid attendanceId)
        {
            var attendance = await _attendancesRepository.GetAttendance(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            return attendance.ToAttendanceResponse();
        }

        public async Task<List<AttendanceResponseDTO>> GetAttendancesByDate(DateTime date)
        {
            var attendances = await _attendancesRepository.GetAttendancesByDate(date);
            return attendances.Select(a => a.ToAttendanceResponse()).ToList();
        }
    }
}
