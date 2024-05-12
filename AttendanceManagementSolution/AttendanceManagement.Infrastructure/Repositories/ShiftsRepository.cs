using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceManagement.Infrastructure.Repositories
{
    public class ShiftsRepository : IShiftsRepository
    {
        private readonly ApplicationDbContext _db;
        public ShiftsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Shift?> DeleteShift(Guid shiftId)
        {
            var shift = await _db.Shifts.FindAsync(shiftId);
            if (shift == null)
            {
                return null;
            }
            _db.Shifts.Remove(shift);
            await _db.SaveChangesAsync();
            return shift;
        }

        public async Task<List<Shift>> GetAllShifts()
        {
            return await _db.Shifts.ToListAsync();
        }

        public async Task<Shift?> GetShift(Guid shiftId)
        {
            var shift = await _db.Shifts.FirstOrDefaultAsync(d => d.ShiftId == shiftId);
            return shift;
        }

        public async Task<Shift?> UpdateShift(Shift shift)
        {
            var matchingShift = await _db.Shifts.FirstOrDefaultAsync(d => d.ShiftId == shift.ShiftId);

            if (matchingShift == null)
            {
                return null;
            }

            matchingShift.Time_In = shift.Time_In;
            matchingShift.Time_Out = shift.Time_Out;

            int countUpdated = await _db.SaveChangesAsync();

            return matchingShift;
        }
    }
}
