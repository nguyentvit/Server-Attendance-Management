using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.DayOffDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface IDayOffUsersRepository
    {
        Task<List<DayOffUser>> GetAllDayOffUser();
        Task<DayOffUser?> AddDayOffUser(DayOffUser dayOffUser);
        Task<List<DayOffUser>> GetAllDayOffUserByUserId(Guid UserId);
        Task<DayOffUser?> GetDayOffUserByUserIdAndDate(Guid userId, DateTime date);
        Task<List<DayOffUser>> GetAllDayOffAdminByDate(DateTime date);
        Task<DayOffUser?> UpdateDayOff(DayOffUpdateDTO dayOffUpdateDTO);
    }
}
