using BAL.DTOs.SADTO;
using BLL.ControllerSide.SuperAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly SuperAdmin _superAdminService;

        public SuperAdminController(SuperAdmin superAdminService)
        {
            _superAdminService = superAdminService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisDTO dTO)
        {
            var result = await _superAdminService.RegisterUser(dTO.mail, dTO.passwd, dTO.companyName);
            if (result == "User registered successfully.")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Company")]
        public async Task<IActionResult> Company()
        {
            var companies = await _superAdminService.GetCompanies();
            return Ok(companies);
        }

        [HttpGet("Requests")]
        public async Task<IActionResult> Requests()
        {
            var requests = await _superAdminService.GetCompanyRequests();
            return Ok(requests);
        }

        [HttpGet("ResetPassw")]
        public async Task<IActionResult> ResetPassw(string mail, string psw)
        {
            var result = await _superAdminService.ResetPassword(mail, psw);
            if (result.Succeeded)
            {
                return Ok("Password reset successfully.");
            }
            return BadRequest("Password reset failed.");
        }
    }
}
