using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.SalaryDTO;
using AttendanceManagement.Core.DTO.SalaryPayDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.Identity;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Services
{
    public class SalaryPayService : ISalaryPayService
    {
        private readonly ISalaryPaysRepository _salaryPaysRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAttendanceService _attendanceService;
        private readonly ISalaryService _salaryService;
        public SalaryPayService(ISalaryPaysRepository salaryPaysRepository, UserManager<ApplicationUser> userManager, IAttendanceService attendanceService, ISalaryService salaryService)
        {
            _salaryPaysRepository = salaryPaysRepository;
            _userManager = userManager;
            _attendanceService = attendanceService;
            _salaryService = salaryService;
        }

        public async Task<SalaryPayResponseDTO?> AddSalaryPay(SalaryPay salaryPay)
        {
            var salaryPayAdder = await _salaryPaysRepository.AddSalaryPay(salaryPay);
            return salaryPayAdder?.ToSalaryPayResponseDTO();
        }

        public Task<List<SalaryPayResponseDTO>> GetSalaryPayByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<SalaryPayResponseDTO?> GetSalaryPayByUserIdAndTime(Guid userId, DateTime time)
        {
            var salaryPay = await _salaryPaysRepository.GetSalaryPayByUserIdAndTime(userId, time);
            return salaryPay?.ToSalaryPayResponseDTO();
        }

        private async Task<double> SumaryHourPerMonth(Guid userId, DateTime time)
        {
            var attendances = await _attendanceService.GetAttendancesByUserIdAndDate(userId, time);
            double sum = 0;
            for(int i = 0; i < attendances.Count - 1; i++)
            {
                if (attendances[i].Status && !attendances[i+1].Status)
                {
                    var timeWork = (attendances[i + 1].Time - attendances[i].Time).TotalHours;
                    sum += timeWork;
                }
            }
            return sum;
        }
        public async Task<List<SalaryPayResponseDTO>> GetSalaryPays()
        {
            var users = await GetUsersWithRoleUser();
            List<SalaryResponseDTO> salary = await _salaryService.GetSalaries();
            var salaryDetail = salary.FirstOrDefault();
            var salaryCost = salaryDetail.SalaryPerHour;
            foreach (var user in users)
            {
                DateTime time = DateTime.Now;
                DateTime? dateStart = DateTime.Now;
                if (dateStart.HasValue  && salaryCost != null)
                {
                    DateTime iterationDate = dateStart.Value;
                    while(iterationDate <= time)
                    {
                        var salaryPayByUserIdAndTime = await GetSalaryPayByUserIdAndTime(user.Id, iterationDate);
                        var sumaryHourPerMonth = await SumaryHourPerMonth(user.Id, iterationDate);
                        var sumaryMoney = sumaryHourPerMonth * salaryCost;
                        if (salaryPayByUserIdAndTime == null)
                        {
                            SalaryPay salaryPayAdd = new SalaryPay()
                            {
                                UserId = user.Id,
                                Month = iterationDate,
                                MoneyReceive = 0,
                                MoneyPay = sumaryMoney,
                                SumaryHour = sumaryHourPerMonth,
                            };
                            var salaryPayAdder = await AddSalaryPay(salaryPayAdd);
                        }
                        else
                        {
                            salaryPayByUserIdAndTime.MoneyPay = sumaryMoney;
                            salaryPayByUserIdAndTime.SumaryHour = sumaryHourPerMonth;
                            await UpdateSalaryPay(new SalaryPay() 
                            { 
                                Month = salaryPayByUserIdAndTime.Month,
                                MoneyPay = salaryPayByUserIdAndTime.MoneyPay,
                                MoneyReceive = salaryPayByUserIdAndTime.MoneyPay,
                                UserId = salaryPayByUserIdAndTime.UserId,
                                SumaryHour = salaryPayByUserIdAndTime.SumaryHour
                            });
                        }
                        int startYear = iterationDate.Year;
                        int startMonth = iterationDate.Month;

                        if (iterationDate.Month == 12)
                        {
                            iterationDate = new DateTime(iterationDate.Year + 1, 1, 1);
                        }

                        else
                        {
                            iterationDate = new DateTime(iterationDate.Year, iterationDate.Month + 1, 1);
                        }
                    }
                }

                //var salaryPayByUserIdAndTime = await GetSalaryPayByUserIdAndTime(user.Id, user.DateStart);
            }
            var salaryPay = await _salaryPaysRepository.GetSalaryPays();
            return salaryPay.Select(sp => sp.ToSalaryPayResponseDTO()).ToList();
        }

        public async Task<SalaryPayResponseDTO> UpdateSalaryPay(SalaryPay salaryPay)
        {
            var salaryUpdated = await _salaryPaysRepository.UpdateSalaryPay(salaryPay);
            return salaryUpdated.ToSalaryPayResponseDTO();
        }
        private async Task<List<ApplicationUser>> GetUsersWithRoleUser()
        {
            var usersInRoleUser = await _userManager.GetUsersInRoleAsync(UserTypeOptions.User.ToString());

            var usersWithDepartment = await _userManager.Users.Include(u => u.Department).ToListAsync();

            var users = usersWithDepartment.Where(u => usersInRoleUser.Any(u2 => u2.Id == u.Id)).ToList();

            return users;
        }

        public async Task<List<SalaryPayResponseDTO>> GetSalaryPaysByDate(DateTime date)
        {
            var users = await GetUsersWithRoleUser();
            List<SalaryResponseDTO> salary = await _salaryService.GetSalaries();
            var salaryDetail = salary.FirstOrDefault();
            var salaryCost = salaryDetail.SalaryPerHour;
            foreach (var user in users)
            {
                DateTime time = DateTime.Now;
                DateTime? dateStart = DateTime.Now;
                if (dateStart.HasValue  && salaryCost != null)
                {
                    DateTime iterationDate = dateStart.Value;
                    while (iterationDate <= time)
                    {
                        var salaryPayByUserIdAndTime = await GetSalaryPayByUserIdAndTime(user.Id, iterationDate);
                        var sumaryHourPerMonth = await SumaryHourPerMonth(user.Id, iterationDate);
                        var sumaryMoney = sumaryHourPerMonth * salaryCost;
                        if (salaryPayByUserIdAndTime == null)
                        {
                            SalaryPay salaryPayAdd = new SalaryPay()
                            {
                                UserId = user.Id,
                                Month = iterationDate,
                                MoneyReceive = 0,
                                MoneyPay = sumaryMoney,
                                SumaryHour = sumaryHourPerMonth,
                            };
                            var salaryPayAdder = await AddSalaryPay(salaryPayAdd);
                        }
                        else
                        {
                            salaryPayByUserIdAndTime.MoneyPay = sumaryMoney;
                            salaryPayByUserIdAndTime.SumaryHour = sumaryHourPerMonth;
                            await UpdateSalaryPay(new SalaryPay()
                            {
                                Month = salaryPayByUserIdAndTime.Month,
                                MoneyPay = salaryPayByUserIdAndTime.MoneyPay,
                                MoneyReceive = salaryPayByUserIdAndTime.MoneyPay,
                                UserId = salaryPayByUserIdAndTime.UserId,
                                SumaryHour = salaryPayByUserIdAndTime.SumaryHour
                            });
                        }
                        int startYear = iterationDate.Year;
                        int startMonth = iterationDate.Month;

                        if (iterationDate.Month == 12)
                        {
                            iterationDate = new DateTime(iterationDate.Year + 1, 1, 1);
                        }

                        else
                        {
                            iterationDate = new DateTime(iterationDate.Year, iterationDate.Month + 1, 1);
                        }
                    }
                }

                //var salaryPayByUserIdAndTime = await GetSalaryPayByUserIdAndTime(user.Id, user.DateStart);
            }
            var salaryPay = await _salaryPaysRepository.GetSalaryPayByTime(date);
            return salaryPay.Select(sp => sp.ToSalaryPayResponseDTO()).ToList();
        }
    }
}
