using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.AttendanceDTO
{
    public class AttendanceAddDTO
    {
        [Required(ErrorMessage = "Status can't be blank")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "UserId can't be blank")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "PathImg can't be blank")]
        public int PathImg { get; set; }
    }
}
