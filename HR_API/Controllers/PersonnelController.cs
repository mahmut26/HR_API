using BAL.DTOs.AdminDelete;
using BAL.DTOs.CreateDTO;
using BAL.DTOs.SADTO;
using BAL.DTOs.UpdateDTO;
using BLL.ControllerSide.Personnel;
using BLL.ControllerSide.SuperAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly PersonnelCreate _personnelCreate;
        private readonly PersonnelRead _personnelGet;
        private readonly PersonnelDelete _personnelDelete;
        private readonly PersonnelUpdate _personnelUpdate;
        private readonly SuperAdmin superAdmin;
        public PersonnelController(PersonnelCreate personnelCreate, PersonnelRead personnelGet, PersonnelDelete personnelDelete, PersonnelUpdate personnelUpdate, SuperAdmin superAdmin)
        {
            _personnelGet = personnelGet;
            _personnelCreate = personnelCreate;
            _personnelDelete = personnelDelete;
            _personnelUpdate = personnelUpdate;
            this.superAdmin = superAdmin;
        }

        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo(UpdateInfoDTO dTO)
        {
            var result = await _personnelUpdate.UpdateInfo(dTO.mail, dTO.name, dTO.des, dTO.dateTime);

            return Ok(result);
        }

        [HttpPut("UpdateLeave")]
        public async Task<IActionResult> UpdateLeave(UpdateLeaveDTO dTO)
        {
            var result = await _personnelUpdate.UpdateLeave(dTO.mail, dTO.LeaveType, dTO.StartDate, dTO.EndingDate, dTO.idLeave);

            return Ok(result);
        }

        [HttpPut("UpdateExperience")]
        public async Task<IActionResult> UpdateExperience(UpdateExperienceDTO dTO)
        {
            var result = await _personnelUpdate.UpdateExperience(dTO.mail, dTO.Position, dTO.Company, dTO.StartDate, dTO.EndingDate, dTO.id);

            return Ok(result);
        }

        [HttpPut("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseDTO dto)
        {
            var result = await _personnelUpdate.UpdateExpense(dto.mail, dto.description, dto.total, dto.idExp);

            return Ok(result);
        }

        [HttpPut("UpdateEducation")]
        public async Task<IActionResult> UpdateEducation(UpdateEducationDTO dTO)
        {
            var result = await _personnelUpdate.UpdateEducation(dTO.mail, dTO.nameofEdu, dTO.dateofEdu, dTO.id);

            return Ok(result);
        }

        [HttpDelete("DeleteLeave")]
        public async Task<IActionResult> DeleteLeave(DeleteLeaveDTO dTO)
        {
            var result = await _personnelDelete.DeleteLeave(dTO.mail, dTO.leaveid);

            return Ok(result);
        }

        [HttpDelete("DeleteExperience")]
        public async Task<IActionResult> DeleteExperience(DeleteExprerienceDTO dTO)
        {
            var result = await _personnelDelete.DeleteExperience(dTO.mail, dTO.experienceId);

            return Ok(result);
        }

        [HttpDelete("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(DeleteExpenseDTO dTO)
        {
            var result = await _personnelDelete.DeleteExpense(dTO.mail, dTO.expenseId);

            return Ok(result);
        }

        [HttpDelete("DeleteEducation")]
        public async Task<IActionResult> DeleteEducation(DeleteEducationDTO dTO)
        {
            var result = await _personnelDelete.DeleteEducation(dTO.mail, dTO.eduId);

            return Ok(result);
        }

        [HttpGet("UserFromTables")]
        public async Task<IActionResult> UserFromTables(string userId, int req)
        {
            var result = await _personnelGet.UsersFromTab(userId, req);

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
            var result = await _personnelCreate.AddInfo(dTO.id, dTO.name, dTO.des, dTO.dateTime);

            return Ok(result);
        }

        [HttpPost("Leave")]
        public async Task<IActionResult> AddLeave(AddLeaveDTO dTO)
        {
            var result = await _personnelCreate.AddLeave(dTO.id, dTO.LeaveType, dTO.StartDate, dTO.EndingDate);

            return Ok(result);
        }

        [HttpPost("Experience")]
        public async Task<IActionResult> AddExperience(AddExperienceDTO dTO)
        {
            var result = await _personnelCreate.AddExperience(dTO.id, dTO.Position, dTO.Company, dTO.StartDate, dTO.EndingDate);

            return Ok(result);
        }

        [HttpPost("Expense")]
        public async Task<IActionResult> AddExpense(AddExpenseDTO dTO)
        {
            var result = await _personnelCreate.AddExpense(dTO.id, dTO.Description, dTO.Total);

            return Ok(result);
        }

        [HttpPost("Education")]
        public async Task<IActionResult> AddEducation(AddEducationDTO dTO)
        {
            var result = await _personnelCreate.AddEducation(dTO.id, dTO.NameofEdu, dTO.DateofEdu);

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
