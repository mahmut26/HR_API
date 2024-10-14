using BAL.Database.DatabaseIdentity;
using BAL.UnitOfWork;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ControllerSide.Manager
{
    public class ManagerRead
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        public ManagerRead(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;
        }

        public async Task<IActionResult> GetUsersByRToId(string getbyMail, string rToMail, int request)
        {

            var user = await _userManager.FindByEmailAsync(getbyMail);

            if (user == null)
            {
                return null;
            }

            if (user.reportToMail != rToMail)
            {
                return new BadRequestObjectResult("not in order");
            }

            var info = await _unitOfWork.userInfo.GetAllAsync(x => x.userId == user.Id);

            var experiences = await _unitOfWork.experience.GetAllAsync(x => x.userId == user.Id);

            var educations = await _unitOfWork.education.GetAllAsync(x => x.userId == user.Id);

            var leaves = await _unitOfWork.leaves.GetAllAsync(x => x.userId == user.Id);

            var expenses = await _unitOfWork.expenses.GetAllAsync(x => x.userId == user.Id);

            switch (request)
            {
                case 1:
                    return new OkObjectResult(info.ToList());

                case 2:
                    return new OkObjectResult(experiences.ToList());

                case 3:
                    return new OkObjectResult(educations.ToList());

                case 4:
                    return new OkObjectResult(leaves.ToList());

                case 5:
                    return new OkObjectResult(expenses.ToList());

                default:
                    return new NotFoundObjectResult("Invalid request.");
            }

        }

        public async Task<IActionResult> GetUserByRToId(string reportToMail)
        {
            var users = await _unitOfWork.users.GetAllAsync();

            var userRet = users.Where(x => x.reportToMail == reportToMail).ToList();

            if (userRet.Count == 0)
            {
                return new BadRequestObjectResult("No users");
            }

            return new OkObjectResult(userRet);

        }

        public async Task<IActionResult> GetUnApprovedReq(string rToMail)
        {
            // Get all users who report to the specified email
            var users = await _unitOfWork.users.GetAllAsync();
            var userRet = users.Where(x => x.reportToMail == rToMail).ToList();

            if (!userRet.Any())
            {
                return new NotFoundObjectResult("No users found reporting to this email.");
            }

            var leaveIds = userRet.Select(u => u.Id).ToList();
            var unapprovedLeaves = await _unitOfWork.leaves.GetAllAsync(x => leaveIds.Contains(x.userId) && !x.Approval);

            var expensesIds = userRet.Select(u => u.Id).ToList();
            var unapprovedExpenses = await _unitOfWork.expenses.GetAllAsync(x => expensesIds.Contains(x.userId) && !x.Approval);

            var result = new
            {
                UnapprovedLeaves = unapprovedLeaves,
                UnapprovedExpenses = unapprovedExpenses
            };

            return new OkObjectResult(result);
        }

    }
}
