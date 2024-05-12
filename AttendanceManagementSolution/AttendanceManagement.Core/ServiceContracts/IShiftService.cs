using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.ShiftDTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IShiftService
    {
        Task<List<ShiftResponseDTO>> GetAllShifts();
        Task<ShiftResponseDTO?> GetShift(Guid shiftId);
        Task<ShiftResponseDTO?> UpdateShift(Shift shift);
    }
}
