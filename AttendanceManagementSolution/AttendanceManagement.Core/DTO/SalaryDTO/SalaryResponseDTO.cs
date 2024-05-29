using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.DTO.SalaryDTO
{
    public class SalaryResponseDTO
    {
        public Guid SalaryId {  get; set; }
        public double SalaryPerHour {  get; set; }
    }
    public static class SalaryExtension
    {
        public static SalaryResponseDTO ToSalaryResponseDTO(this Salary salary)
        {
            return new SalaryResponseDTO()
            {
                SalaryId = salary.SalaryId,
                SalaryPerHour = salary.SalaryPerHour
            };
        }
    }
}
