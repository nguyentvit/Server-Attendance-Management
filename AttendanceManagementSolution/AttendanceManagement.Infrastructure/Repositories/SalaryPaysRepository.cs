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
    public class SalaryPaysRepository : ISalaryPaysRepository
    {
        private readonly ApplicationDbContext _db;
        public SalaryPaysRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SalaryPay?> AddSalaryPay(SalaryPay salaryPay)
        {
            _db.SalaryPays.Add(salaryPay); 
            await _db.SaveChangesAsync();
            return salaryPay;
        }

        public async Task<List<SalaryPay>> GetSalaryPayByTime(DateTime time)
        {
            var salaryPayByTime = await _db.SalaryPays.Include(u => u.User).Where(sp => sp.Month.Month == time.Month && sp.Month.Year == time.Year).ToListAsync();
            return salaryPayByTime;
        }

        public async Task<List<SalaryPay>> GetSalaryPayByUserId(Guid userId)
        {
            var salaryPayByUserId = await _db.SalaryPays.Include(u => u.User).Where(sp => sp.UserId == userId).ToListAsync();
            return salaryPayByUserId;
        }

        public async Task<SalaryPay?> GetSalaryPayByUserIdAndTime(Guid userId, DateTime time)
        {
            var salaryPayByUserIdAndTime = await _db.SalaryPays.Include(u => u.User).Where(sp => sp.UserId == userId && sp.Month == time).FirstOrDefaultAsync();
            return salaryPayByUserIdAndTime;
        }

        public async Task<List<SalaryPay>> GetSalaryPays()
        {
            return await _db.SalaryPays.Include(u => u.User).ToListAsync();
        }

        public async Task<SalaryPay> UpdateSalaryPay(SalaryPay salaryPay)
        {
            SalaryPay? matchingSalaryPay = await _db.SalaryPays.FirstOrDefaultAsync(temp => temp.UserId == salaryPay.UserId && temp.Month.Year == salaryPay.Month.Year && temp.Month.Month == salaryPay.Month.Month);
            
            if (matchingSalaryPay != null)
            {
                matchingSalaryPay.MoneyPay = salaryPay.MoneyPay;
                matchingSalaryPay.MoneyReceive = salaryPay.MoneyReceive;
                matchingSalaryPay.SumaryHour = salaryPay.SumaryHour;
                int countUpdated = await _db.SaveChangesAsync();
                return matchingSalaryPay;
            }

            return salaryPay;
        }
    }
}
