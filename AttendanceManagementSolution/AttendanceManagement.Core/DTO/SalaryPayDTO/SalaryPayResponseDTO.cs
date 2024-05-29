using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.SalaryPayDTO
{
    public class SalaryPayResponseDTO
    {
        public Guid UserId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public Guid? DepartmentId { get; set; }
        public string DepartmentName {  get; set; } = string.Empty;
        public DateTime Month {  get; set; }
        public double MoneyPay { get; set; }
        public double MoneyReceive { get; set; }
        public double SumaryHour { get; set; }
    }
    public static class SalaryPayExtention
    {
        public static SalaryPayResponseDTO ToSalaryPayResponseDTO(this SalaryPay salaryPay)
        {
            return new SalaryPayResponseDTO()
            {
                UserId = salaryPay.UserId,
                PersonName = salaryPay.User.PersonName,
                DepartmentId = salaryPay.User.DeparmentId,
                DepartmentName = salaryPay.User.Department.DepartmentName,
                Month = salaryPay.Month,
                MoneyPay = salaryPay.MoneyPay,
                MoneyReceive = salaryPay.MoneyReceive,
                SumaryHour = salaryPay.SumaryHour
            };
        }
    }
}
