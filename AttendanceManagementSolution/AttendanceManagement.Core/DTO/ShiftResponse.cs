using AttendanceManagement.Core.Domain.Entities;
using System;

namespace AttendanceManagement.Core.DTO
{
    public class ShiftResponse
    {
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeSpan Time_In {  get; set; }
        public TimeSpan Time_Out { get; set; }
        public Guid DepartmentId {  get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(ShiftResponse))
            {
                return false;
            }

            ShiftResponse shift_to_compare = (ShiftResponse)obj;
            return ShiftId == shift_to_compare.ShiftId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class ShiftExtentions
    {
        public static ShiftResponse ToShiftResponse(this Shift shift)
        {
            Department department = shift.Department;
            return new ShiftResponse
            {
                ShiftId = shift.ShiftId,
                ShiftName = shift.ShiftName,
                Time_In = shift.Time_In,
                Time_Out = shift.Time_Out,
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }
    }
}
