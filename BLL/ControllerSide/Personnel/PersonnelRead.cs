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

namespace BLL.ControllerSide.Personnel
{
    public class PersonnelRead
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        public PersonnelRead(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;
        }

        public async Task<IActionResult> UsersFromTab(string mail, int request)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null)
            {
                return null;
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
    }
}
