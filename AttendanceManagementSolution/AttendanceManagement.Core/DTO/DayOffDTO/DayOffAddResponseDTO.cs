using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DayOffDTO
{
    public class DayOffAddResponseDTO
    {
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason {  get; set; } = string.Empty;
    }
    public static class DayOffAddResponseDTOExtensions
    {
        public static DayOffAddResponseDTO ToDayOffAddResponseDTO(this DayOffUser dayOffUser)
        {
            return new DayOffAddResponseDTO()
            {
                Date = dayOffUser.DayOff.Date,
                Status = dayOffUser.Status,
                Reason = dayOffUser.Reason,
            };
        }
    }
}
