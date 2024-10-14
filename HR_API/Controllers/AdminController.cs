using BAL.DTOs;
using BAL.DTOs.AdminDelete;
using BAL.DTOs.SADTO;
using BAL.DTOs.UpdateDTO;
using BLL.ControllerSide.Admin;
using BLL.ControllerSide.Manager;
using BLL.ControllerSide.Personnel;
using BLL.ControllerSide.SuperAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminDelete AdminDelete;
        private readonly AdminUpdate AdminUpdate;
        private readonly AdminCreate adminCreate;
        private readonly AdminRead adminRead;
        private readonly ManagerUpdate managerUpdate;
        private readonly PersonnelDelete personnelDelete;
        private readonly PersonnelUpdate personnelUpdate;
        private readonly SuperAdmin superAdmin;
        public AdminController(AdminCreate adminCreate, AdminRead adminRead, AdminDelete AdminDelete, AdminUpdate AdminUpdate,ManagerUpdate managerUpdate, PersonnelDelete personnelDelete, PersonnelUpdate personnelUpdate, SuperAdmin superAdmin)
        {
            this.adminCreate = adminCreate;
            this.adminRead = adminRead;
            this.AdminDelete = AdminDelete;
            this.AdminUpdate = AdminUpdate;
            this.managerUpdate = managerUpdate;
            this.personnelDelete = personnelDelete;
            this.personnelUpdate = personnelUpdate;
            this.superAdmin = superAdmin;
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserDTO dTO)
        {
            var res = await this.AdminDelete.DeleteUser(dTO.mail, dTO.cID);
            return Ok(res);
        }

        [HttpPatch("RoleUpdate")]
        public async Task<IActionResult> RoleUpdate(RoleUpdateDTO dTO)
        {
            var res = await this.AdminUpdate.RoleUpdate(dTO.toBeUpdate, dTO.roleName);
            return Ok(res);
        }

        [HttpPatch("ReportToUpdate")]
        public async Task<IActionResult> ReportToUpdate(ReportToUpdateDTO dTO)
        {
            var res = await this.AdminUpdate.ReportToUpdate(dTO.toBeUpdate, dTO.reportTo, dTO.cId);
            return Ok(res);
        }

        [HttpPatch("RoleNReportUpdate")]
        public async Task<IActionResult> RoleNReportUpdate(RoleNReportDTO dTO)
        {
            var res = await this.AdminUpdate.RoleNReportUpdate(dTO.toBeUpdate, dTO.reportTo, dTO.cId, dTO.roleName);
            return Ok(res);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserCreateDTO dto)
        {
            var res = await adminCreate.RegisterUser(dto);
            return res;
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
            var result = await personnelUpdate.UpdateExperience(dTO.mail, dTO.Position, dTO.Company, dTO.StartDate, dTO.EndingDate,dTO.id);

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
            var result = await personnelUpdate.UpdateEducation(dTO.mail, dTO.nameofEdu, dTO.dateofEdu,dTO.id);

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
            var result = await personnelDelete.DeleteEducation(dTO.mail,dTO.eduId);

            return Ok(result);
        }

        [HttpGet("GetUnApprovedReq")]
        public async Task<IActionResult> GetUnApprovedReq(string rToMail)
        {
            var result = await adminRead.GetUnApprovedReq(rToMail);

            return Ok(result);
        }

        [HttpGet("ReadRequested")]
        public async Task<IActionResult> GetUsersByRToId(string userMail, int request, int cId)
        {
            var res = await adminRead.GetUsersByRToId(userMail, request, cId);
            return res;
        }

        [HttpGet("ReadManager")]
        public async Task<IActionResult> GetUserByRToId(string userMail)
        {
            var res = await adminRead.GetUserByRToId(userMail);
            return res;
        }

        [HttpGet("ReadAll")]
        public async Task<IActionResult> GetByCompId(int cId)
        {
            var res = await adminRead.GetByCompId(cId);
            return res;
        }

        [HttpGet("ReadRole")]
        public async Task<IActionResult> GetRole(string userMail)
        {
            var res = await adminRead.GetRole(userMail);
            return res;
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

