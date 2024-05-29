using AttendanceManagement.Core.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Core.Domain.Entities
{
    public class SalaryPay
    {
        [Required(ErrorMessage = "Month can't be blank")]
        public DateTime Month {  get; set; }
        [Required(ErrorMessage = "Money pay can't be blank")]
        public double MoneyPay {  get; set; }
        [Required(ErrorMessage = "Money receive can't be blank")]
        public double MoneyReceive { get; set; }
        [Required(ErrorMessage = "SummaryHour can't be blank")]
        public double SumaryHour { get; set; }
        [Required]
        public Guid UserId {  get; set; }
        public ApplicationUser User { get; set; } = null!;

    }
}
