using AttendanceManagement.Core.Domain.Entities;
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
        }
    }
}
