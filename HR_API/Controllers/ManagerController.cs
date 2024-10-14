using BAL.DTOs;
using BAL.DTOs.AdminDelete;
using BAL.DTOs.CreateDTO;
using BAL.DTOs.SADTO;
using BAL.DTOs.UpdateDTO;
using BLL.ControllerSide.Manager;
using BLL.ControllerSide.Personnel;
using BLL.ControllerSide.SuperAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerRead managerRead;
        private readonly ManagerUpdate managerUpdate;
        private readonly PersonnelCreate personnelCreate;
        private readonly PersonnelDelete personnelDelete;
        private readonly PersonnelUpdate personnelUpdate;
        private readonly PersonnelRead personnelRead;
        private readonly SuperAdmin superAdmin;

        public ManagerController(PersonnelDelete personnelDelete, PersonnelUpdate personnelUpdate, ManagerRead managerRead, ManagerUpdate managerUpdate, PersonnelCreate personnelCreate, PersonnelRead personnelRead, SuperAdmin superAdmin)
        {
            this.personnelDelete = personnelDelete;
            this.personnelUpdate = personnelUpdate;
            this.personnelCreate = personnelCreate;
            this.managerRead = managerRead;
            this.managerUpdate = managerUpdate;
            this.personnelRead = personnelRead;
            this.superAdmin = superAdmin;
        }
        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo(UpdateInfoDTO dTO)
        {
            var result = await personnelUpdate.UpdateInfo(dTO.mail, dTO.name, dTO.des, dTO.dateTime);

            return Ok(result);
        }

        [HttpPut("UpdateLeave")]
        public async Task<IActionResult> UpdateLeave(UpdateLeaveDTO dTO)
        {
            var result = await personnelUpdate.UpdateLeave(dTO.mail, dTO.LeaveType, dTO.StartDate, dTO.EndingDate, dTO.idLeave);

            return Ok(result);
        }

        [HttpPut("UpdateExperience")]
        public async Task<IActionResult> UpdateExperience(UpdateExperienceDTO dTO)
        {
            var result = await personnelUpdate.UpdateExperience(dTO.mail, dTO.Position, dTO.Company, dTO.StartDate, dTO.EndingDate, dTO.id);

            return Ok(result);
        }

        [HttpPut("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseDTO dto)
        {
            var result = await personnelUpdate.UpdateExpense(dto.mail, dto.description, dto.total, dto.idExp);

            return Ok(result);
        }

        [HttpPut("UpdateEducation")]
        public async Task<IActionResult> UpdateEducation(UpdateEducationDTO dTO)
        {
            var result = await personnelUpdate.UpdateEducation(dTO.mail, dTO.nameofEdu, dTO.dateofEdu, dTO.id);

            return Ok(result);
        }

        [HttpDelete("DeleteLeave")]
        public async Task<IActionResult> DeleteLeave(DeleteLeaveDTO dTO)
        {
            var result = await personnelDelete.DeleteLeave(dTO.mail, dTO.leaveid);

            return Ok(result);
        }

        [HttpDelete("DeleteExperience")]
        public async Task<IActionResult> DeleteExperience(DeleteExprerienceDTO dTO)
        {
            var result = await personnelDelete.DeleteExperience(dTO.mail, dTO.experienceId);

            return Ok(result);
        }

        [HttpDelete("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(DeleteExpenseDTO dTO)
        {
            var result = await personnelDelete.DeleteExpense(dTO.mail, dTO.expenseId);

            return Ok(result);
        }

        [HttpDelete("DeleteEducation")]
        public async Task<IActionResult> DeleteEducation(DeleteEducationDTO dTO)
        {
            var result = await personnelDelete.DeleteEducation(dTO.mail, dTO.eduId);

            return Ok(result);
        }

        [HttpGet("UserFromTables")]
        public async Task<IActionResult> UserFromTables(string userMail, int req)
        {
            var result = await personnelRead.UsersFromTab(userMail, req);


            if (result is NotFoundObjectResult notFoundResult)
            {
                return NotFound(notFoundResult.Value);
            }

            if (result is BadRequestObjectResult badRequestResult)
            {
                return BadRequest(badRequestResult.Value);
            }


            return Ok(result);
        }


        [HttpPost("Info")]
        public async Task<IActionResult> AddInfo(AddInfoDTO dTO)
        {
            var result = await personnelCreate.AddInfo(dTO.id, dTO.name, dTO.des, dTO.dateTime);

            return Ok(result);
        }

        [HttpPost("Leave")]
        public async Task<IActionResult> AddLeave(AddLeaveDTO dTO)
        {
            var result = await personnelCreate.AddLeave(dTO.id, dTO.LeaveType, dTO.StartDate, dTO.EndingDate);

            return Ok(result);
        }

        [HttpPost("Experience")]
        public async Task<IActionResult> AddExperience(AddExperienceDTO dTO)
        {
            var result = await personnelCreate.AddExperience(dTO.id, dTO.Position, dTO.Company, dTO.StartDate, dTO.EndingDate);

            return Ok(result);
        }

        [HttpPost("Expense")]
        public async Task<IActionResult> AddExpense(AddExpenseDTO dTO)
        {
            var result = await personnelCreate.AddExpense(dTO.id, dTO.Description, dTO.Total);

            return Ok(result);
        }

        [HttpPost("Education")]
        public async Task<IActionResult> AddEducation(AddEducationDTO dTO)
        {
            var result = await personnelCreate.AddEducation(dTO.id, dTO.NameofEdu, dTO.DateofEdu);

            return Ok(result);
        }
        [HttpGet("GetUnApprovedReq")]
        public async Task<IActionResult> GetUnApprovedReq(string rToMail)
        {
            var result = await managerRead.GetUnApprovedReq(rToMail);

            return Ok(result);
        }
        [HttpGet("GetUserByRToId")]
        public async Task<IActionResult> GetUserByRToId(string reportToMail)
        {
            var result = await managerRead.GetUserByRToId(reportToMail);

            return Ok(result);
        }

        [HttpGet("GetUsersByRToId")]
        public async Task<IActionResult> GetUsersByRToId(string getbyMail, string rToMail, int request)
        {
            var result = await managerRead.GetUsersByRToId(getbyMail, rToMail, request);

            return Ok(result);
        }

        [HttpPatch("ApproveLeave")]
        public async Task<IActionResult> ApproveLeave(ApproveLeaveDTO dTO)
        {
            var result = await managerUpdate.ApproveLeave(dTO.leaveId, dTO.idForRtoId, dTO.isApp);
            return Ok(result);
        }

        [HttpPatch("ApproveExpense")]
        public async Task<ActionResult<ResponseDTO>> ApproveExpense(ApproveExpenseDTO dTO)
        {
            var result = await managerUpdate.ApproveExpense(dTO.leaveId, dTO.idForRtoId, dTO.isApp);
            return Ok(result);
        }

        [HttpGet("ResetPassw")]
        public async Task<IActionResult> ResetPassw(string mail, string psw)
        {
            var result = await superAdmin.ResetPassword(mail, psw);
            if (result.Succeeded)
            {
                return Ok("Password reset successfully.");
            }
            return BadRequest("Password reset failed.");
        }
    }
}

