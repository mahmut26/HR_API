using BAL.Database.DatabaseIdentity;
using BAL.Database.RequestCompany;
using BAL.UnitOfWork;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ControllerSide.Login
{
    public class Login
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IUnitOfWork unitOfWork;

        public Login(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            this.unitOfWork = unitOfWork;   
        }

        public async Task<IActionResult> Logina(string email,string password)
        {

            var result = await _signInManager.PasswordSignInAsync(email,password,false,false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new NotFoundObjectResult("User not found.");
                }

                var token = await _tokenService.GenerateTokenAsync(user);
                return new OkObjectResult(new { Token = token });
            }

            return new UnauthorizedObjectResult("Invalid login attempt");
        }

        public async Task<IActionResult> Request(string mail,string CompanyName)
        {
            var comp = new NewCompanyRequest { CompanyName = CompanyName,Mail=mail };

            await unitOfWork.reqComp.AddAsync(comp);

            await unitOfWork.SaveAsync();

            return new OkObjectResult("We will be back");

        }

    }
}
