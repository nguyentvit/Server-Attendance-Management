using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.DayOffDTO
{
    public class DayOffUpdateDTO
    {
        public string status {  get; set; } = string.Empty;
        public DateTime date {  get; set; }
        public Guid userId { get; set; }
        public string reason { get; set; } = string.Empty;
    }
}
