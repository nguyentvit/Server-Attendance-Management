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
    public class SalariesRepository : ISalariesRepository
    {
        private readonly ApplicationDbContext _db;
        public SalariesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Salary>> GetSalaries()
        {
            return await _db.Salaries.ToListAsync();
        }

        public async Task<Salary> UpdateSalary(Salary salary)
        {
            var salaryUpdated = await _db.Salaries.FirstOrDefaultAsync(s => s.SalaryId == salary.SalaryId);
            if (salaryUpdated != null)
            {
                salaryUpdated.SalaryPerHour = salary.SalaryPerHour;
                await _db.SaveChangesAsync();
                return salaryUpdated;
            }
            return salary;
        }
    }
}
