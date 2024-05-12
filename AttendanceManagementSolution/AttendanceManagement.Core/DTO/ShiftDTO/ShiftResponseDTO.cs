using AttendanceManagement.Core.Domain.Entities;
using System;

namespace AttendanceManagement.Core.DTO.ShiftDTO
{
    public class ShiftResponseDTO
    {
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeSpan Time_In { get; set; }
        public TimeSpan Time_Out { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(ShiftResponseDTO))
            {
                return false;
            }

            ShiftResponseDTO shift_to_compare = (ShiftResponseDTO)obj;
            return ShiftId == shift_to_compare.ShiftId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class ShiftExtentions
    {
        public static ShiftResponseDTO ToShiftResponse(this Shift shift)
        {
            return new ShiftResponseDTO
            {
                ShiftId = shift.ShiftId,
                ShiftName = shift.ShiftName,
                Time_In = shift.Time_In,
                Time_Out = shift.Time_Out
            };
        }
    }
}
