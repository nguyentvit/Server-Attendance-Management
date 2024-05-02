using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Repositories
{
    public class AttendancesRepository : IAttendancesRepository
    {
        private readonly ApplicationDbContext _db;
        public AttendancesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Attendance> AddAttendance(Attendance Attendance)
        {
            _db.Attendances.Add(Attendance);
            await _db.SaveChangesAsync();

            var attendance = await GetAttendance(Attendance.AttendanceId);
            if (attendance == null)
            {
                return Attendance;
            }
            return attendance;
        }

        public async Task<List<Attendance>> GetAllAttendances()
        {
            return await _db.Attendances.Include(a => a.User).ToListAsync();
        }

        public async Task<Attendance?> GetAttendance(Guid AttendanceId)
        {
            var attendance = await _db.Attendances.Include(a => a.User).FirstOrDefaultAsync(a => a.AttendanceId == AttendanceId);
            return attendance;
        }

        public async Task<List<Attendance>> GetAttendancesByDate(DateTime date)
        {
            var attendance = await _db.Attendances.Include(a => a.User).Where(a => a.Time.Date == date.Date).ToListAsync();
            return attendance;
        }
    }
}
