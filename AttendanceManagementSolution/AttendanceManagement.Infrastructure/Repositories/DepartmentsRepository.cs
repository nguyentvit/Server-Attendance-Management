using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
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

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _db.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentByDepartmentId(Guid departmentId)
        {
            return await _db.Departments.FirstOrDefaultAsync(temp => temp.DepartmentId == departmentId);
        }

        public Task<List<Department>> GetFilteredDepartments(Expression<Func<Department, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            Department? matchingDepartment = await _db.Departments.FirstOrDefaultAsync(temp => temp.DepartmentId == department.DepartmentId);

            if (matchingDepartment == null) 
            {
                return department;
            }

            matchingDepartment.DepartmentName = department.DepartmentName;
            int countUpdated = await _db.SaveChangesAsync();

            return matchingDepartment;
        }
    }
}
