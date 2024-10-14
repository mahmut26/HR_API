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

namespace BLL.ControllerSide.Admin
{
    public class AdminUpdate
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        public AdminUpdate(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;
        }

        public async Task<IActionResult> RoleUpdate(string toBeUpdate, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(toBeUpdate);

            if (roleName == "Admin" || roleName == "SuperAdmin" || user == null)
            {
                return new BadRequestObjectResult("You cannot do that !!");
            }

            await _roleService.AssignRoleToUserAsync(user, roleName);

            return new OkObjectResult("Done");
        }

        public async Task<IActionResult> ReportToUpdate(string toBeUpdateMail, string userMail, int cId)
        {
            var user = await _userManager.FindByEmailAsync(toBeUpdateMail);

            var assignUser = await _userManager.FindByEmailAsync(userMail);

            if (assignUser == null || assignUser.CompanyID != cId || user == null)
            {
                return new BadRequestObjectResult("You cannot do that !!");
            }

            user.reportToMail = userMail;

            await _userManager.UpdateAsync(user);

            return new OkObjectResult("Done");
        }

        public async Task<IActionResult> RoleNReportUpdate(string toBeUpdateMail, string reportToMail, int cId, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(toBeUpdateMail);

            var assignUser = await _userManager.FindByEmailAsync(reportToMail);

            if (assignUser == null || assignUser.CompanyID != cId || user == null || roleName == "Admin" || roleName == "SuperAdmin")
            {
                return new BadRequestObjectResult("You cannot do that !!");
            }

            user.reportToMail = reportToMail;

            await _userManager.UpdateAsync(user);

            await _roleService.AssignRoleToUserAsync(user, roleName);

            return new OkObjectResult("Done");
        }
    }
}
