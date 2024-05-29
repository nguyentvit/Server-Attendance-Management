using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface ISalariesRepository
    {
        Task<List<Salary>> GetSalaries();
        Task<Salary> UpdateSalary(Salary salary);
    }
}
