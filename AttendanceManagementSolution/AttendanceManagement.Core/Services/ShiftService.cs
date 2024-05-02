using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.ServiceContracts;
using System;

namespace AttendanceManagement.Core.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftsRepository _shiftsRepository;
        public ShiftService(IShiftsRepository shiftsRepository)
        {
            _shiftsRepository = shiftsRepository;
        }

        public async Task<ShiftResponse?> AddShift(Shift shift)
        {
            await _shiftsRepository.AddShift(shift);
            return shift.ToShiftResponse();
        }

        public async Task<List<ShiftResponse>> GetAllShifts()
        {
            List<Shift> shifts = await _shiftsRepository.GetAllShifts();
            return shifts.Select(s => s.ToShiftResponse()).ToList();
        }

        public async Task<ShiftResponse?> GetShift(Guid shiftId)
        {
            Shift? shift = await _shiftsRepository.GetShift(shiftId);
            if (shift == null)
            {
                return null;
            }
            return shift.ToShiftResponse();
        }

        public async Task<ShiftResponse?> UpdateShift(Shift shift)
        {
            var shiftUpdate = await _shiftsRepository.UpdateShift(shift);
            if (shiftUpdate == null) 
            {
                return null;
            }
            return shiftUpdate.ToShiftResponse();
        }
    }
}
