using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.DayOffDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Repositories
{
    public class DayOffUsersRepository : IDayOffUsersRepository
    {
        private readonly ApplicationDbContext _db;
        public DayOffUsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DayOffUser?> AddDayOffUser(DayOffUser dayOffUser)
        {
            var dayOffUserAdder = await _db.DayOffUsers.Where(d => d.UserId == dayOffUser.UserId && d.DayOffId == dayOffUser.DayOffId).FirstOrDefaultAsync();
            if (dayOffUserAdder == null)
            {
                dayOffUser.Status = DayOffOptions.Waiting.ToString();
                _db.DayOffUsers.Add(dayOffUser);
                await _db.SaveChangesAsync();
                return await _db.DayOffUsers.Where(d => d.UserId == dayOffUser.UserId && d.DayOffId == dayOffUser.DayOffId).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<DayOffUser>> GetAllDayOffAdminByDate(DateTime date)
        {
            return await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Include(u => u.User.Department).Where(d => d.DayOff.Date == date).ToListAsync();
        }

        public async Task<List<DayOffUser>> GetAllDayOffAdminByUserId(Guid userId)
        {
            return await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Include(u => u.User.Department).Where(d => d.UserId ==  userId).ToListAsync();
        }

        public async Task<List<DayOffUser>> GetAllDayOffUser()
        {
            return await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Include(u => u.User.Department).ToListAsync();
        }

        public async Task<List<DayOffUser>> GetAllDayOffUserByUserId(Guid UserId)
        {
            return await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Where(d => d.UserId == UserId).ToListAsync();
        }

        public async Task<DayOffUser?> GetDayOffUserByUserIdAndDate(Guid userId, DateTime date)
        {
            return await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Where(d => d.UserId == userId && d.DayOff.Date == date).FirstOrDefaultAsync();
        }

        public async Task<DayOffUser?> UpdateDayOff(DayOffUpdateDTO dayOffUpdateDTO)
        {
            DateTime date = dayOffUpdateDTO.date;
            Guid userId = dayOffUpdateDTO.userId;
            string status = dayOffUpdateDTO.status;
            string reason = dayOffUpdateDTO.reason;
            var dayOffUpdate = await _db.DayOffUsers.Include(u => u.User).Include(d => d.DayOff).Where(d => d.UserId == userId && d.DayOff.Date == date).FirstOrDefaultAsync();
            if (dayOffUpdate == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(status))
            {
                dayOffUpdate.Status = status;
            }
            if (!string.IsNullOrEmpty(reason))
            {
                if (dayOffUpdate.Status == DayOffOptions.Allow.ToString() || dayOffUpdate.Status == DayOffOptions.Disallow.ToString())
                {
                    return null;
                }
                dayOffUpdate.Reason = reason;
            }
            int countUpdated = await _db.SaveChangesAsync();
            return dayOffUpdate;
        }
    }
}
