using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DayOffDTO
{
    public class DayOffAddDTO
    {
        public DateTime Date {  get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
