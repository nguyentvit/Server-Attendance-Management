using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceManagement.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public ApplicationDbContext() { }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<DayOff> DayOffs { get; set; }
        public virtual DbSet<DayOffUser> DayOffUsers { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<SalaryPay> SalaryPays { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>().HasData(new Department()
            {
                DepartmentId = Guid.Parse("C64523A0-99E0-4959-9820-BBF0B036205E"),
                DepartmentName = "IT"
            });

            builder.Entity<Department>().HasData(new Department()
            {
                DepartmentId = Guid.Parse("EF90FD8F-AAEA-444E-B3A0-78B56D6C51F8"),
                DepartmentName = "Accounting"
            });

            builder.Entity<Shift>().HasData(new Shift()
            {
                ShiftId = Guid.Parse("13A27559-42BE-4984-8E5C-55331F5A039F"),
                ShiftName = ShiftOptions.Morning.ToString(),
                Time_In = new TimeSpan(7, 30, 0),
                Time_Out = new TimeSpan(11, 30, 0)
            });

            builder.Entity<Shift>().HasData(new Shift()
            {
                ShiftId = Guid.Parse("3A6FFEDC-25D6-45DA-8354-B7880ECED953"),
                ShiftName = ShiftOptions.Afternoon.ToString(),
                Time_In = new TimeSpan(14, 0, 0),
                Time_Out = new TimeSpan(17, 30, 0)
            });

            builder.Entity<Salary>().HasData(new Salary()
            {
                SalaryId = Guid.Parse("735CDC2A-C28D-436C-A544-4247BFCDF27F"),
                SalaryPerHour = 20000
            });


            builder.Entity<Department>()
                .HasMany(d => d.Users)
                .WithOne(d => d.Department)
                .HasForeignKey(u => u.DeparmentId)
                .IsRequired(false);

            builder.Entity<Attendance>()
                .HasOne(a => a.User)
                .WithMany(a => a.Attendances)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.Entity<DayOffUser>().HasKey(d => new { d.UserId, d.DayOffId });

            builder.Entity<DayOffUser>()
                .HasOne<DayOff>(d => d.DayOff)
                .WithMany(s => s.DayOffUsers)
                .HasForeignKey(d => d.DayOffId);

            builder.Entity<DayOffUser>()
                .HasOne<ApplicationUser>(d => d.User)
                .WithMany(u => u.DayOffUsers)
                .HasForeignKey(u => u.UserId);

            builder.Entity<DayOffUser>()
                .Property(d => d.Status)
                .HasDefaultValue(DayOffOptions.Waiting.ToString());

            builder.Entity<SalaryPay>()
                .HasKey(sp => new { sp.Month, sp.UserId });

            builder.Entity<SalaryPay>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.SalaryPays)
                .HasForeignKey(sp => sp.UserId);
            
        }
    }
}
