using AttendanceManagement.Core.DTO.UserDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersAdminController : CustomControllersAdminBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersAdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReponse>>> GetAllUsers()
        {
            var usersInRoleUser = await _userManager.GetUsersInRoleAsync(UserTypeOptions.User.ToString());
            
            var usersWithDepartment = await _userManager.Users.Include(u => u.Department).ToListAsync();

            var users = usersWithDepartment.Where(u => usersInRoleUser.Any(u2 => u2.Id == u.Id));

            var userResponse = users.Select(u => new UserReponse()
            {
                Id = u.Id,
                PersonName = u.PersonName,
                Email = u.Email,
                Gender = u.Gender,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber,
                DepartmentId = (Guid)u.DeparmentId,
                DepartmentName = u.Department.DepartmentName
            });

            return Ok(userResponse);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponseWithAttendance>> GetUser(Guid userId)
        {
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        //[HttpGet("{userId}/{date}")]
        //public async Task<IActionResult> GetAttendanceOfUserByDate(Guid userId, string date)
        //{
        //    var usersInRoleUser = await _userManager.GetUsersInRoleAsync(UserTypeOptions.User.ToString());

        //    if (usersInRoleUser == null)
        //    {
        //        return NotFound();
        //    }

        //    DateTime dateTime;
        //    if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        //    {
        //        return BadRequest();
        //    }

        //    var attendances = await _attendanceService.GetAttendancesByDate(dateTime);

        //    var attendancesOfUser = attendances.Where(a => a.UserId == userId).ToList();

        //    return Ok(attendancesOfUser);
        //}
    }
}
