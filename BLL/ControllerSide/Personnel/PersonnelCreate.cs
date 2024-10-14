using BAL.Database.DatabaseIdentity;
using BAL.Database.DatabaseModels;
using BAL.UnitOfWork;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ControllerSide.Personnel
{
    public class PersonnelCreate
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        private readonly MailSenderService _emailService;

        public PersonnelCreate(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService, MailSenderService emailService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;

            _emailService = emailService;
        }

        public async Task<IActionResult> AddInfo(string mail, string name, string des, DateTime dateTime)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null) 
            {
                return new BadRequestObjectResult("No User Found");
            }

            var isHere = await _unitOfWork.userInfo.GetAllAsync();

            var Here = isHere.FirstOrDefault(x => x.userId == user.Id);

            if (Here == null)
            {
                var info = new UserInfo
                {
                    userId = user.Id,

                    bornDate = dateTime,

                    Name = name,

                    Description = des,
                };
                await _unitOfWork.userInfo.AddAsync(info);

                await _unitOfWork.SaveAsync();

                //  await _emailService.MailSenderByID(id);

                return new OkObjectResult("added");
            }
            else
            {
                return new BadRequestObjectResult("allready here better go for update");
            }
        }

        public async Task<IActionResult> AddLeave(string mail, string LeaveType, DateTime StartDate, DateTime EndingDate)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null||user.reportToMail==null)
            {
                return new BadRequestObjectResult("No User Found");
            }
            if (EndingDate <= StartDate)
            {
                return new BadRequestObjectResult("Invalid start or end date.");
            }
            var leave = new Leave
            {
                userId = user.Id,

                LeaveType = LeaveType,

                EndingDate = EndingDate,

                StartDate = StartDate,

                reportTo = user.reportToMail
            };

            var totalLeaveDaysRequested = (EndingDate - StartDate).Days;

            if (user.rLeav < totalLeaveDaysRequested)
            {
                return new BadRequestObjectResult("insufficient days");
            }

            await _unitOfWork.leaves.AddAsync(leave);

            await _unitOfWork.SaveAsync();

            // await _emailService.MailSenderByID(id);

            return new OkObjectResult("added");


        }

        public async Task<IActionResult> AddExperience(string mail, string Position, string Company, DateTime StartDate, DateTime EndingDate)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("No User Found");
            }
            var exp = new Experience
            {
                Company = Company,

                EndingDate = EndingDate,

                StartDate = StartDate,

                Position = Position,

                userId = user.Id
            };

            await _unitOfWork.experience.AddAsync(exp);

            await _unitOfWork.SaveAsync();

            // await _emailService.MailSenderByID(id);

            return new OkObjectResult("ok");
        }

        public async Task<IActionResult> AddExpense(string mail, string Description, decimal Total)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null||user.reportToMail==null)
            {
                return new BadRequestObjectResult("No User Found");
            }

            var expense = new Expense
            {
                Description = Description,

                Total = Total,

                userId = user.Id,

                reportTo = user.reportToMail
            };

            await _unitOfWork.expenses.AddAsync(expense);

            await _unitOfWork.SaveAsync();

            //await _emailService.MailSenderByID(id);

            return new OkObjectResult("ok");
        }

        public async Task<IActionResult> AddEducation(string mail, string NameofEdu, DateTime DateofEdu)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null || user.reportToMail == null)
            {
                return new BadRequestObjectResult("No User Found");
            }
            var education = new Education
            {
                NameofEdu = NameofEdu,

                DateofEdu = DateofEdu,

                userId = user.Id
            };



            await _unitOfWork.education.AddAsync(education);

            await _unitOfWork.SaveAsync();

            //await _emailService.MailSenderByID(id);

            return new OkObjectResult("ok");
        }
    }
}
