using AttendanceManagement.Core.DTO.WorkingStatusDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IWorkingStatusService
    {
        Task<WorkingStatusReponseDTO?> WorkingStatusUser(Guid userId, DateTime dateTime);
        Task<List<WorkingStatusReponseDTO>> WorkingStatusAllUsers(DateTime dateTime);
    }
}
