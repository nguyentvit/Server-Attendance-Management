using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.WorkingStatusDTO;
using AttendanceManagement.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Services
{
    public class WorkingStatusService : IWorkingStatusService
    {
        private readonly IAttendancesRepository _attendancesRepository;
        public WorkingStatusService(IAttendancesRepository attendancesRepository)
        {
            _attendancesRepository = attendancesRepository;
        }

        public async Task<List<WorkingStatusReponseDTO>> WorkingStatusAllUsers(DateTime dateTime)
        {
            var result = await _attendancesRepository.WorkingStatusAllUsers(dateTime);
            var listWorkingStatusResponses = new List<WorkingStatusReponseDTO>();
            foreach (var response in result)
            {
                if (response.AttendanceId == Guid.Empty)
                {
                    listWorkingStatusResponses.Add(new WorkingStatusReponseDTO()
                    {
                        userId = response.UserId,
                        status = "Out",
                        time = response.Time,
                    });
                }
                else
                {
                    listWorkingStatusResponses.Add(response.ToWorkingStatusReponseDTO());
                }
            }
            return listWorkingStatusResponses;
        }

        public async Task<WorkingStatusReponseDTO?> WorkingStatusUser(Guid userId, DateTime dateTime)
        {
            var result = await _attendancesRepository.WorkingStatusUser(userId, dateTime);
            if (result == null)
            {
                return new WorkingStatusReponseDTO()
                {
                    userId = userId,
                    status = "Out",
                    time = DateTime.MinValue
                };
            }
            return result.ToWorkingStatusReponseDTO();
        }
    }
}
