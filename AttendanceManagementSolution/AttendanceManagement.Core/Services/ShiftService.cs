using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.ShiftDTO;
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
        public async Task<List<ShiftResponseDTO>> GetAllShifts()
        {
            List<Shift> shifts = await _shiftsRepository.GetAllShifts();
            return shifts.Select(s => s.ToShiftResponse()).ToList();
        }

        public async Task<ShiftResponseDTO?> GetShift(Guid shiftId)
        {
            Shift? shift = await _shiftsRepository.GetShift(shiftId);
            if (shift == null)
            {
                return null;
            }
            return shift.ToShiftResponse();
        }

        public async Task<ShiftResponseDTO?> UpdateShift(Shift shift)
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
