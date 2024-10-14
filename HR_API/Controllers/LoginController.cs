using BAL.DTOs.LoginDTO;
using BLL.ControllerSide.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Login _login;

        public LoginController(Login login)
        {
            _login = login;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOLOG dTOLOG)
        {
            var res = await _login.Logina(dTOLOG.a,dTOLOG.b);
            return Ok(res);
        }
        [HttpPost("Request")]
        public async Task<IActionResult> Requ(DTOLOG dTOLOG)
        {
            var res = await _login.Request(dTOLOG.a, dTOLOG.b);
            return Ok(res);
        }
    }
}
