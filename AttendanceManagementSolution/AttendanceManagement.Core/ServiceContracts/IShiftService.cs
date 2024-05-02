using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IShiftService
    {
        Task<List<ShiftResponse>> GetAllShifts();
        Task<ShiftResponse?> GetShift(Guid shiftId);
        Task<ShiftResponse?> AddShift(Shift shift);
        Task<ShiftResponse?> UpdateShift(Shift shift);
    }
}
