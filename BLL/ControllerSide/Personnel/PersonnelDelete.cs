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
    public class PersonnelDelete
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        public PersonnelDelete(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;
        }

        public async Task<IActionResult> DeleteLeave(string mail, int leavid)
        {
            var user=await _userManager.FindByEmailAsync(mail);

            if(user == null)
            {
                return new BadRequestObjectResult("no user Found"); 
            }

            var leaveReq = await _unitOfWork.leaves.GetAllAsync(x => x.userId == user.Id);

            var control = leaveReq.Where(x => x.Approval != true).Where(x => x.Id == leavid).ToList();

            if (control.Count == 0)
            {
                return new OkObjectResult("It is Approved. Cant be deleted.");
            }

            await _unitOfWork.leaves.DeleteAsync(leavid);

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("deleted");

        }

        public async Task<IActionResult> DeleteExperience(string mail, int experid)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("no user Found");
            }

            await _unitOfWork.experience.DeleteAsync(experid);

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("deleted");

        }

        public async Task<IActionResult> DeleteExpense(string mail, int expnceid)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("no user Found");
            }

            var expReq = await _unitOfWork.expenses.GetAllAsync(x => x.userId == user.Id);

            var control = expReq.Where(x => x.Approval != true).Where(x => x.Id == expnceid).ToList();

            if (control.Count == 0)
            {
                return new OkObjectResult("It is Approved. Cant be deleted.");
            }



            await _unitOfWork.expenses.DeleteAsync(expnceid);


            await _unitOfWork.SaveAsync();

            return new OkObjectResult("deleted");

        }

        public async Task<IActionResult> DeleteEducation(string mail, int eduid)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                return new BadRequestObjectResult("no user Found");
            }

            await _unitOfWork.education.DeleteAsync(eduid);

            await _unitOfWork.SaveAsync();

            return new OkObjectResult("deleted");

        }
    }
}
