using AttendanceManagement.Core.DTO.UserDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "User")]
    public class UsersController : CustomControllersUserBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<ActionResult<UserResponseWithAttendance>> GetUser()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usersInRoleUser = await _userManager.GetUsersInRoleAsync(UserTypeOptions.User.ToString());

            var usersWithDepartment = await _userManager.Users.Include(u => u.Department).Include(u => u.Attendances).ToListAsync();

            var user = usersWithDepartment.Where(u => usersInRoleUser.Any(u2 => u2.Id == u.Id)).Where(u => u.Id == userId).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var userResponse = new UserResponseWithAttendance()
            {
                Id = user.Id,
                PersonName = user.PersonName,
                Email = user.Email,
                Gender = user.Gender,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DepartmentId = (Guid)user.DeparmentId,
                DepartmentName = user.Department.DepartmentName,
            };

            List<Dictionary<string, string>> attendances = user.Attendances.Select(a => new Dictionary<string, string>
            {
                { "AttendanceId", a.AttendanceId.ToString() },
                { "Time", a.Time.ToString() },
                {"Status", a.Status.ToString() },
            }).ToList();

            userResponse.Attendances = attendances;
            return Ok(userResponse);
        }
    }
}
