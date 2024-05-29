using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<Attendance> GetLastAttendance()
        {
            var attendance = await _db.Attendances.Include(a => a.User).OrderBy(d => d.Time).LastOrDefaultAsync();

            var personName = attendance.User.PersonName;
            var normalName = RemoveVietnameseDiacritics(personName);
            attendance.User.PersonName = normalName;
            return attendance;
        }
        private string RemoveVietnameseDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            return regex.Replace(normalizedString, System.String.Empty)
                        .Replace('\u0111', 'd')
                        .Replace('\u0110', 'D');
        }
        public async Task<List<Attendance>> WorkingStatusAllUsers(DateTime date)
        {
            var roleUserId = await _db.Roles.Where(r => r.Name == "User").Select(r => r.Id).FirstOrDefaultAsync();
            var userIds = await _db.UserRoles.Where(r => r.RoleId == roleUserId).Select(u => u.UserId).ToListAsync();

            List<Attendance> attendances = new List<Attendance>();
            foreach (var userId in  userIds)
            {
                
                var attendance = await _db.Attendances.Include(a => a.User).Where(d => d.UserId == userId && d.Time < date).OrderBy(d => d.Time).LastOrDefaultAsync();
                var lastTwoAttendances = await _db.Attendances
                    .Include(a => a.User)
                    .Where(d => d.UserId == userId && d.Time < date)
                    .OrderByDescending(d => d.Time)
                    .Take(2)
                    .OrderBy(d => d.Time)
                    .ToListAsync();
                
                if (attendance == null)
                {
                    attendance = new Attendance()
                    {
                        AttendanceId = Guid.Empty,
                        UserId = userId,
                        Time = DateTime.MinValue,
                        Status = false
                    };
                    attendances.Add(attendance);
                }
                else
                {
                    foreach (Attendance a in lastTwoAttendances)
                    {
                        attendances.Add(a);
                    }
                }
                //if (attendance == null)
                //{
                //    attendance = new Attendance()
                //    {
                //        AttendanceId = Guid.Empty,
                //        UserId = userId,
                //        Time = DateTime.MinValue,
                //        Status = false
                //    };
                //    attendances.Add(attendance);
                //}
                //else
                //{
                //    attendances.Add(attendance);
                //}
            }
            return attendances;
        }

        public async Task<Attendance?> WorkingStatusUser(Guid userId, DateTime date)
        {
            var attendance = await _db.Attendances.Include(a => a.User).Where(d => d.UserId == userId && d.Time < date).OrderBy(d => d.Time).LastOrDefaultAsync();
            return attendance;

        }

        public async Task<List<Attendance>> GetAttendancesByUserId(Guid userId)
        {
            var attendance = await _db.Attendances.Include(a => a.User).Where(d => d.UserId == userId).OrderBy(d => d.Time).ToListAsync();
            return attendance;
        }

        public async Task<List<Attendance>> GetAttendancesByUserIdAndDate(Guid userId, DateTime date)
        {
            var attendance = await _db.Attendances.Include(a => a.User).Where(d => d.UserId == userId && d.Time.Year == date.Year && d.Time.Month == date.Month).OrderBy(d => d.Time).ToListAsync();
            return attendance;
        }
    }
}
