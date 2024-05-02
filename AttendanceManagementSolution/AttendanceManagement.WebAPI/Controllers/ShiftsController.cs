using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ShiftsController : CustomControllersBase
    {
        private readonly IShiftService _shiftService;
        private readonly IDepartmentService _departmentService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shiftService"></param>
        public ShiftsController(IShiftService shiftService, IDepartmentService departmentService)
        {
            _shiftService=shiftService;
            _departmentService = departmentService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftResponse>>> GetAllShifts()
        {
            var shifts = await _shiftService.GetAllShifts();
            return Ok(shifts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        [HttpGet("{shiftId}")]
        public async Task<ActionResult<ShiftResponse>> GetShift(Guid shiftId)
        {
            var shift = await _shiftService.GetShift(shiftId);
            if (shift == null)
            {
                return NoContent();
            }
            return Ok(shift);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shiftDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ShiftResponse>> AddShift(ShiftDTO shiftDTO)
        {
            DepartmentResponse? department = await _departmentService.GetDepartment(shiftDTO.DepartmentId);

            if (department == null)
            {
                return NotFound();
            }

            if (department.Shifts.Any(d => d.ShiftName.Equals(shiftDTO.ShiftName.ToString())))
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Shift already existed in the department",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }

            string[] time_ins = shiftDTO.Time_In.Split(':');
            string[] time_outs = shiftDTO.Time_Out.Split(":");

            if (time_ins.Length != 2 || time_outs.Length != 2)
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_In or Time_Out was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }
            TimeSpan time_in;
            if (time_ins.Length == 2 && int.TryParse(time_ins[0], out int hours_in) && int.TryParse(time_ins[1], out int minutes_in))
            {
                time_in = new TimeSpan(hours_in, minutes_in, 0);
            }

            else
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_In was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }

            TimeSpan time_out;
            if (time_outs.Length == 2 && int.TryParse(time_outs[0], out int hours_out) && int.TryParse(time_outs[1], out int minutes_out))
            {
                time_out = new TimeSpan(hours_out, minutes_out, 0);
            }

            else
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_Out was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }
            Shift shift = new Shift()
            {
                ShiftName = shiftDTO.ShiftName.ToString(),
                DepartmentId = shiftDTO.DepartmentId,
                Time_In = time_in,
                Time_Out = time_out,
            };
            await _shiftService.AddShift(shift);
            return CreatedAtAction("GetShift", new
            {
                shiftId = shift.ShiftId
            }, shift.ToShiftResponse());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="shiftUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{shiftId}")]
        public async Task<ActionResult<ShiftResponse>> PutShift(Guid shiftId, ShiftUpdateDTO shiftUpdateDTO)
        {
            ShiftResponse? shiftUpdate = await _shiftService.GetShift(shiftId);
            if (shiftUpdate == null) 
            {
                return NotFound();
            }

            string[] time_ins = shiftUpdateDTO.Time_In.Split(':');
            string[] time_outs = shiftUpdateDTO.Time_Out.Split(":");

            if (time_ins.Length != 2 || time_outs.Length != 2)
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_In or Time_Out was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }
            TimeSpan time_in;
            if (time_ins.Length == 2 && int.TryParse(time_ins[0], out int hours_in) && int.TryParse(time_ins[1], out int minutes_in))
            {
                time_in = new TimeSpan(hours_in, minutes_in, 0);
            }

            else
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_In was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }

            TimeSpan time_out;
            if (time_outs.Length == 2 && int.TryParse(time_outs[0], out int hours_out) && int.TryParse(time_outs[1], out int minutes_out))
            {
                time_out = new TimeSpan(hours_out, minutes_out, 0);
            }

            else
            {
                CustomRequestResponse response = new CustomRequestResponse()
                {
                    title = "Time_Out was not in a correct format",
                    status = 400,
                    errors = null
                };
                return BadRequest(response);
            }

            Shift shift = new Shift()
            {
                ShiftId = shiftUpdate.ShiftId,
                Time_In = time_in,
                Time_Out = time_out
            };

            ShiftResponse? result = await _shiftService.UpdateShift(shift);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
