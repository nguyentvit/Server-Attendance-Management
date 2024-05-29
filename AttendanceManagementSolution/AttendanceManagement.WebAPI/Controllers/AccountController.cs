using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.DTO.CustomDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AccountController : CustomControllersAdminBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IDepartmentService _departmentService;
        private readonly IJwtService _jwtService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtService"></param>
        /// <param name="departmentService"></param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IDepartmentService departmentService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _departmentService=departmentService;
        }
        /// <summary>
        /// Register account by fields (PersonName), (Email), (Gender: Male, Female, Other), (Address), (PhoneNumber), (Password), (ConfirmPassword)
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            //checkEmailExisted
            var userEmailExisted = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userEmailExisted != null)
            {
                ModelState.AddModelError("Email", "Email is already in use");
            }

            //checkPhoneNumberExisted
            var userPhoneNumberExisted = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDTO.PhoneNumber);
            if (userPhoneNumberExisted != null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is already in use");
            }

            if (ModelState.ErrorCount > 0)
            {
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    ).Where(m => m.Value.Any());

                var customResponse = new CustomRequestResponse()
                {
                    errors = errorList.ToDictionary(kv => kv.Key, kv => kv.Value),
                    status = 400,
                    title = "One or more validation errors occurred"

                };
                return BadRequest(customResponse);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                Gender = registerDTO.Gender,
                Address = registerDTO.Address
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.Admin.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else if (registerDTO.UserType == UserTypeOptions.User)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.User.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }

                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                ModelState.AddModelError("Error", errorMessage);
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Login by fields (Email), (Password)
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            if (ModelState.ErrorCount > 0)
            {
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    ).Where(m => m.Value.Any());

                var customResponse = new CustomRequestResponse()
                {
                    errors = errorList.ToDictionary(kv => kv.Key, kv => kv.Value),
                    status = 400,
                    title = "One or more validation errors occurred"

                };
                return BadRequest(customResponse);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return NoContent();
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> 
                { 
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _jwtService.CreateJwtToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    status = 200
                });
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return BadRequest(new CustomRequestResponse()
                    {
                        status = 400,
                        title = "Account is locked",
                        errors = new Dictionary<string, string[]>
                        {
                            { "Lockout", new string[] { "Your account is locked. Please try again later." } }
                        }
                    });
                }
                else if (result.IsNotAllowed)
                {
                    return BadRequest(new CustomRequestResponse()
                    {
                        status = 400,
                        title = "Account is not allowed",
                        errors = new Dictionary<string, string[]>
                        {
                            { "NotAllowed", new string[] { "Your account is not allowed to login." } }
                        }
                    });
                }
                else
                {
                    return BadRequest(new CustomRequestResponse()
                    {
                        status = 400,
                        title = "Invalid email or password",
                        errors = new Dictionary<string, string[]>
                        {
                            { "InvalidCredentials", new string[] { "Invalid email or password." } }
                        }
                    });
                }
            }
        }
        /// <summary>
        /// Log out
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("registerUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApplicationUser>> PostRegisterUser(RegisterDTO registerDTO)
        {
            //checkEmailExisted
            var userEmailExisted = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (userEmailExisted != null)
            {
                ModelState.AddModelError("Email", "Email is already in use");
            }

            //checkPhoneNumberExisted
            var userPhoneNumberExisted = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerDTO.PhoneNumber);
            if (userPhoneNumberExisted != null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is already in use");
            }

            DateTime dateValue;
            if (!DateTime.TryParseExact(registerDTO.DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new { error = "Invalid date format. Date must be in 'yyyy-MM-dd' format." });
            }

            if (ModelState.ErrorCount > 0)
            {
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    ).Where(m => m.Value.Any());

                var customResponse = new CustomRequestResponse()
                {
                    errors = errorList.ToDictionary(kv => kv.Key, kv => kv.Value),
                    status = 400,
                    title = "One or more validation errors occurred"

                };
                return BadRequest(customResponse);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                Gender = registerDTO.Gender,
                Address = registerDTO.Address,
                DeparmentId = registerDTO.DepartmentId,
                DateStart = dateValue
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.Admin.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else if (registerDTO.UserType == UserTypeOptions.User)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.User.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }
                string userId = user.Id.ToString();
                string userName = user.PersonName.ToString();
                string combine = userId + "_" + userName;
                string folderPath = Path.Combine("E:\\Pbl5\\Recognize\\data\\data_faces_from_camera\\", combine);
                Directory.CreateDirectory(folderPath);

                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                ModelState.AddModelError("Error", errorMessage);
                return BadRequest(ModelState);
            }
        }
    }
}
