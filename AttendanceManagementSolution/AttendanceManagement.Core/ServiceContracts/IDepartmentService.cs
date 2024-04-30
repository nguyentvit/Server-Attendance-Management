using AttendanceManagement.Core.DTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<DepartmentResponse>> GetAllDepartments();
    }
}
