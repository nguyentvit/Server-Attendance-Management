using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface IDepartmentsRepository
    {
        Task<List<Department>> GetAllDepartments();
        Task<Department> AddDepartment(Department department);
        Task<Department?> GetDepartment(Guid departmentId);
        Task<Department?> UpdateDepartment(Department department);
        Task<List<Department>> GetFilteredDepartments(Expression<Func<Department, bool>> predicate);
        Task<Department?> DeleteDepartment(Guid departmentId);
    }
}
