using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.AttendanceDTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IAttendanceService
    {
        Task<List<AttendanceResponseDTO>> GetAllAttendances();
        Task<List<AttendanceResponseDTO>> GetAttendancesByDate(DateTime date);
        Task<AttendanceResponseDTO?> GetAttendance(Guid attendanceId);
        Task<AttendanceResponseDTO> AddAttendance(AttendanceAddDTO attendanceAddDTO);
    }
}
