using BAL.Database.DatabaseIdentity;
using BAL.DTOs;
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
    public class ManagerUpdate
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        private readonly MailSenderService _emailService;


        public ManagerUpdate(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService, MailSenderService emailService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;

            _emailService = emailService;
        }

        public async Task<IActionResult> ApproveLeave(int leaveid, string rToMail, bool isApproved)
        {
            var leaveReq = await _unitOfWork.leaves.GetByIdAsync(leaveid);

            if (leaveReq == null)
            {
                return new NotFoundObjectResult("Leave request not found.");
            }

            if (leaveReq.Approval)
            {
                return new BadRequestObjectResult("Leave request is already approved.");
            }

            var userControl = leaveReq.userId;

            var userWhoReq = await _unitOfWork.users.GetAllAsync(x => x.Id == userControl);

            var user = userWhoReq.ToList();

            if (user[0] == null || user[0].reportToMail != rToMail)
            {
                return new BadRequestObjectResult("bad req");
            }

            var daysOff = (leaveReq.EndingDate - leaveReq.StartDate).TotalDays;

            if (user[0].rLeav.HasValue && user[0].rLeav >= daysOff && isApproved)
            {
                user[0].rLeav -= (int)daysOff; 
                
                await _unitOfWork.users.UpdateAsync(user[0]);

                await _unitOfWork.SaveAsync();

                leaveReq.Approval = true;

                await _unitOfWork.leaves.UpdateAsync(leaveReq);

                await _unitOfWork.SaveAsync();

                return new OkObjectResult("asdase");
            }
            else
            {
                leaveReq.Approval = false;

                await _unitOfWork.leaves.UpdateAsync(leaveReq);

                await _unitOfWork.SaveAsync();

                return new OkObjectResult("ok");
            }



        }

        public async Task<ResponseDTO> ApproveExpense(int expenseId, string reportToMail, bool isApproved)
        {
            var expenseReq = await _unitOfWork.expenses.GetByIdAsync(expenseId);

            if (expenseReq == null)
            {
                return new ResponseDTO { Message = "not found" };
            }

            if (expenseReq.Approval)
            {
                return new ResponseDTO { Message = "exp approved allready." };
            }

            var userControl = expenseReq.userId;

            var userWhoReq = await _unitOfWork.users.GetAllAsync(x => x.Id == userControl);

            var user = userWhoReq.ToList();

            if (user[0] == null || user[0].reportToMail != reportToMail)
            {
                return new ResponseDTO { Message = "Bad REQ." };
            }

            var expense = expenseReq.Total;

            if (isApproved)
            {
                expenseReq.Approval = true;

                user[0].totalExp += expense;

                await _userManager.UpdateAsync(user[0]);

                await _unitOfWork.SaveAsync();

                await _unitOfWork.expenses.UpdateAsync(expenseReq);

                await _unitOfWork.SaveAsync();

                return new ResponseDTO { Message = "Leave request approved." };
            }
            else
            {
                expenseReq.Approval = false;

                await _unitOfWork.expenses.UpdateAsync(expenseReq);

                await _unitOfWork.SaveAsync();

                return new ResponseDTO { Message = "Leave request not." };
            }


        }
    }
}
