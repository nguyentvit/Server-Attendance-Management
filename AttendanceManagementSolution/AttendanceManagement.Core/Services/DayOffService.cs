using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.DayOffDTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Services
{
    public class DayOffService : IDayOffService
    {
        private readonly IDayOffsRepository _dayOffsRepository;
        private readonly IDayOffUsersRepository _dayOffUsersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DayOffService(IDayOffsRepository dayOffsRepository, IDayOffUsersRepository dayOffUsersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dayOffsRepository = dayOffsRepository;
            _dayOffUsersRepository = dayOffUsersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DayOffUser?> AddDayOff(DayOffAddDTO dayOffAddDTO)
        {
            var dayOff = await _dayOffsRepository.AddDayOff(dayOffAddDTO.Date);
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _dayOffUsersRepository.AddDayOffUser(new DayOffUser() { DayOffId = dayOff.DayOffId, UserId = userId, Reason = dayOffAddDTO.Reason });
        }

        public async Task<List<DayOffUser>> GetAllDayOff()
        {
            return await _dayOffUsersRepository.GetAllDayOffUser();
        }

        public async Task<List<DayOffUser>> GetAllDayOffByDate(DateTime date)
        {
            return await _dayOffUsersRepository.GetAllDayOffAdminByDate(date);
        }

        public async Task<List<DayOffUser>> GetAllDayOffUser()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _dayOffUsersRepository.GetAllDayOffUserByUserId(userId);
        }

        public async Task<DayOffUser?> GetDayOffByDate(DateTime date, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return await _dayOffUsersRepository.GetDayOffUserByUserIdAndDate(userId, date);
        }

        public async Task<DayOffUser?> UpdateDayOff(DayOffUpdateDTO dayOffUpdateDTO)
        {
            if (dayOffUpdateDTO.userId == Guid.Empty)
            {
                var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                dayOffUpdateDTO.userId = userId;
            }
            return await _dayOffUsersRepository.UpdateDayOff(dayOffUpdateDTO);
        }
    }
}
