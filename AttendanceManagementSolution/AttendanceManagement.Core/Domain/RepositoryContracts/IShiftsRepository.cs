using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface IShiftsRepository
    {
        Task<List<Shift>> GetAllShifts();
        Task<Shift?> GetShift(Guid shiftId);
        Task<Shift?> UpdateShift(Shift shift);
    }
}
