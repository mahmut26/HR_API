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
    public class AdminDelete
    {
        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        private readonly RoleService _roleService;

        public AdminDelete(UserManager<User> userManager, IUnitOfWork unitOfWork, RoleService roleService)
        {
            _userManager = userManager;

            _unitOfWork = unitOfWork;

            _roleService = roleService;
        }

        public async Task<IActionResult> DeleteUser(string mail, int cId)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user.CompanyID != cId||user==null)
            {
                return new BadRequestObjectResult("Not in our Company or mail wrong");
            }

            await _userManager.DeleteAsync(user);

            return new OkObjectResult("Deleted");
        }
    }
}
