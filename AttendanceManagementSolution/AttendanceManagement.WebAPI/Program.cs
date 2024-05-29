using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.Identity;
using AttendanceManagement.Core.ServiceContracts;
using AttendanceManagement.Core.Services;
using AttendanceManagement.Infrastructure.DatabaseContext;
using AttendanceManagement.Infrastructure.Repositories;
using AttendanceManagement.WebAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.Listen(System.Net.IPAddress.Parse("172.20.10.4"), 5126);
//});

// Add services to the container

builder.Services.AddControllers(options =>
{
    //Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
    options.Filters.Add(new AuthorizeFilter(policy));
})
    .AddXmlSerializerFormatters();

builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IShiftsRepository, ShiftsRepository>();
builder.Services.AddTransient<IShiftService,  ShiftService>();
builder.Services.AddTransient<IAttendancesRepository, AttendancesRepository>();
builder.Services.AddTransient<IAttendanceService,  AttendanceService>();
builder.Services.AddTransient<IDayOffsRepository, DayOffsRepository>();
builder.Services.AddTransient<IDayOffService, DayOffService>();
builder.Services.AddTransient<IDayOffUsersRepository, DayOffUsersRepository>();
builder.Services.AddTransient<IWorkingStatusService, WorkingStatusService>();
builder.Services.AddTransient<ISalariesRepository, SalariesRepository>();
builder.Services.AddTransient<ISalaryService, SalaryService>();
builder.Services.AddTransient<ISalaryPaysRepository, SalaryPaysRepository>();
builder.Services.AddTransient<ISalaryPayService, SalaryPayService>();

builder.Services.AddSignalR();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            //.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
            .WithOrigins("http://127.0.0.1:5501")
            .WithHeaders("Authorization", "origin", "accept", "content-type")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowCredentials()
            .WithHeaders("X-Requested-With", "x-signalr-user-agent");
    });
});

//Identity
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

//JWT
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options => {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
     };
 });

builder.Services.AddAuthorization(options => {
});

var app = builder.Build();

//Configure the Http request pipeline
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<AttendanceHub>("/Attendance");

app.Run();

