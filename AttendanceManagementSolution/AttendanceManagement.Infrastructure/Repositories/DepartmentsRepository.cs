using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.Identity;
using AttendanceManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace AttendanceManagement.Infrastructure.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();

            return department;
        }

        public async Task<Department?> DeleteDepartment(Guid departmentId)
        {
            var department = await _db.Departments.FindAsync(departmentId);
            if (department == null) 
            { 
                return null;
            }

            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();

            return department;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _db.Departments.Include(d => d.Users).Include(d => d.Shifts).ToListAsync();
        }

        public async Task<Department?> GetDepartment(Guid departmentId)
        {
            var department = await _db.Departments.Include(d => d.Users).Include(d => d.Shifts).FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
            return department;
        }

        public Task<List<Department>> GetFilteredDepartments(Expression<Func<Department, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Department?> UpdateDepartment(Department department)
        {
            Department? matchingDepartment = await _db.Departments.FirstOrDefaultAsync(temp => temp.DepartmentId == department.DepartmentId);

            if (matchingDepartment == null) 
            {
                return null;
            }

            matchingDepartment.DepartmentName = department.DepartmentName;
            int countUpdated = await _db.SaveChangesAsync();

            return matchingDepartment;
        }
    }
}
