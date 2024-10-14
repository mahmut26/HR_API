using BAL.Database.DatabaseIdentity;
using BAL.Database.DatabaseModels;
using BAL.UnitOfWork;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ControllerSide.Personnel
{
    public class PersonnelUpdate
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        public PersonnelUpdate(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;
        }

        public async Task<IActionResult> UpdateInfo(string mail, string? name, string? des, DateTime? dateTime)
        {

            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null) 
            {
                return new BadRequestObjectResult("user not found");
            }
            
            var existingInfo = await _unitOfWork.userInfo.GetAllAsync(x => x.userId == user.Id);
            
            var userInfo = existingInfo.FirstOrDefault();

            if (userInfo == null)
            {
                return new NotFoundObjectResult("User info not found. Please create it first.");
            }

            if (name != null)
            {
                userInfo.Name = name;
            }

            if (des != null)
            {
                userInfo.Description = des;
            }

            if (dateTime.HasValue)
            {
                userInfo.bornDate = dateTime.Value;
            }

            await _unitOfWork.userInfo.UpdateAsync(userInfo);

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("updated");
        }

        public async Task<IActionResult> UpdateLeave(string mail, string? leaveType, DateTime? startDate, DateTime? endingDate, int leaveId)
        {

            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }

            var existingLeaves = await _unitOfWork.leaves.GetAllAsync(x => x.userId == user.Id && x.Id == leaveId && x.Approval==false);

            var leaveInfo = existingLeaves.FirstOrDefault();

            if (leaveInfo == null)
            {
                return new NotFoundObjectResult("No existing leave found for this user.");
            }

            if (!startDate.HasValue || !endingDate.HasValue || endingDate <= startDate)
            {
                return new BadRequestObjectResult("Invalid start or end date.");
            }

            var preReq = (leaveInfo.EndingDate - leaveInfo.StartDate).Days;

            var totalLeaveDaysRequested = (endingDate.Value - startDate.Value).Days;

            if (user.rLeav + preReq < totalLeaveDaysRequested)
            {
                return new BadRequestObjectResult("Insufficient leave days.");
            }

            user.rLeav = user.rLeav + preReq - totalLeaveDaysRequested;

            leaveInfo.LeaveType = leaveType ?? leaveInfo.LeaveType;

            leaveInfo.StartDate = startDate.Value;

            leaveInfo.EndingDate = endingDate.Value;

            await _userManager.UpdateAsync(user);

            await _unitOfWork.leaves.UpdateAsync(leaveInfo);

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("Leave updated successfully.");
        }


        public async Task<IActionResult> UpdateExperience(string mail, string? Position, string? Company, DateTime? StartDate, DateTime? EndingDate,int id)
        {

            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }

            var existingExperience = await _unitOfWork.experience.GetAllAsync(x => x.userId == user.Id&&x.Id==id);

            var experienceInfo = existingExperience.FirstOrDefault();

            if (experienceInfo == null)
            {
                return new NotFoundObjectResult("No existing experience found for this user.");
            }

            if (!string.IsNullOrEmpty(Position))
            {
                experienceInfo.Position = Position;
            }

            if (!string.IsNullOrEmpty(Company))
            {
                experienceInfo.Company = Company;
            }

            if (StartDate.HasValue)
            {
                experienceInfo.StartDate = StartDate.Value;
            }

            if (EndingDate.HasValue)
            {
                experienceInfo.EndingDate = EndingDate.Value;
            }

            await _unitOfWork.experience.UpdateAsync(experienceInfo); 

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("Experience updated successfully.");
        }

        public async Task<IActionResult> UpdateExpense(string mail, string? description, decimal? total, int expenseID)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }

            var existingExpense = await _unitOfWork.expenses.GetAllAsync(x => x.userId == user.Id && x.Id == expenseID && x.Approval == false);
            
            var expenseInfo = existingExpense.FirstOrDefault();

            if (expenseInfo == null)
            {
                expenseInfo = new BAL.Database.DatabaseModels.Expense
                {
                    Description = description ?? "No description provided", 
                    
                    Total = total ?? 0, 
                    
                    userId = user.Id
                };
                await _unitOfWork.expenses.AddAsync(expenseInfo);
            }
            else
            {
                if (!string.IsNullOrEmpty(description))
                {
                    expenseInfo.Description = description;
                }

                if (total.HasValue)
                {
                    expenseInfo.Total = total.Value;
                }

                await _unitOfWork.expenses.UpdateAsync(expenseInfo);
            }

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("Expense updated successfully.");
        }

        public async Task<IActionResult> UpdateEducation(string mail, string? nameofEdu, DateTime? dateofEdu,int id)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }
            var existingEducation = await _unitOfWork.education.GetAllAsync(x => x.userId == user.Id && x.Id == id);
            
            var educationInfo = existingEducation.FirstOrDefault();

            if (educationInfo == null)
            {
                educationInfo = new Education
                {
                    NameofEdu = nameofEdu ?? "No education name provided", 

                    DateofEdu = dateofEdu ?? DateTime.MinValue,

                    userId = user.Id
                };

                await _unitOfWork.education.AddAsync(educationInfo);
            }
            else
            {

                if (!string.IsNullOrEmpty(nameofEdu))
                {
                    educationInfo.NameofEdu = nameofEdu;
                }

                if (dateofEdu.HasValue)
                {
                    educationInfo.DateofEdu = dateofEdu.Value;
                }

                await _unitOfWork.education.UpdateAsync(educationInfo);
            }

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("Education updated successfully.");
        }

        public async Task<IdentityResult> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

    }
}
