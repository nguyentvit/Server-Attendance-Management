using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.DayOffDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IDayOffService
    {
        Task<List<DayOffUser>> GetAllDayOff();
        Task<DayOffUser?> AddDayOff(DayOffAddDTO dayOffAddDTO);
        Task<List<DayOffUser>> GetAllDayOffUser();
        Task<DayOffUser?> GetDayOffByDate(DateTime date, Guid userId);
        Task<List<DayOffUser>> GetAllDayOffByDate(DateTime date);
        Task<DayOffUser?> UpdateDayOff(DayOffUpdateDTO dayOffUpdateDTO);
        Task<List<DayOffUser>> GetAllDayOffByUserId(Guid userId);
    }
}
