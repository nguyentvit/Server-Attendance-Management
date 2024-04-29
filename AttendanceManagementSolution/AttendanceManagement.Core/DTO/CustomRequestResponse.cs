using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO
{
    public class CustomRequestResponse
    {
        public int status { get; set; }
        public string title { get; set; } = string.Empty;
        public IDictionary<string, string[]>? errors { get; set; }
    }
}
