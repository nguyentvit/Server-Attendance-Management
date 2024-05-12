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
    public class DayOffsRepository : IDayOffsRepository
    {
        public readonly ApplicationDbContext _db;
        public DayOffsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<DayOff>> GetAllDayOffs()
        {
            return await _db.DayOffs.ToListAsync();
        }

        public async Task<DayOff> AddDayOff(DateTime date)
        {
            DayOff? dayOff = await _db.DayOffs.FirstOrDefaultAsync(d => d.Date == date);
            if (dayOff == null)
            {
                _db.DayOffs.Add(new DayOff() { Date = date });
                await _db.SaveChangesAsync();

            }
            return await _db.DayOffs.FirstOrDefaultAsync(d => d.Date == date);
        }
    }
}
