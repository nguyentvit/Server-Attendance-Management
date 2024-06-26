﻿using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface IAttendancesRepository
    {
        Task<List<Attendance>> GetAllAttendances();
        Task<Attendance?> GetAttendance(Guid AttendanceId);
        Task<Attendance> AddAttendance(Attendance Attendance);
        Task<List<Attendance>> GetAttendancesByDate(DateTime date);
        Task<List<Attendance>> GetAttendancesByUserId(Guid userId);
        Task<List<Attendance>> GetAttendancesByUserIdAndDate(Guid userId, DateTime date);
        Task<Attendance?> WorkingStatusUser(Guid userId, DateTime date);
        Task<List<Attendance>> WorkingStatusAllUsers(DateTime date);
        Task<Attendance> GetLastAttendance();
    }
}
