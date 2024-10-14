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

namespace BLL.ControllerSide.Admin
{
    public class AdminCreate
    {
        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        private readonly RoleService _roleService;

        public AdminCreate(UserManager<User> userManager, IUnitOfWork unitOfWork, RoleService roleService)
        {
            _userManager = userManager;

            _unitOfWork = unitOfWork;

            _roleService = roleService;
        }

        public async Task<IActionResult> RegisterUser(UserCreateDTO dto)
        {
            var company = await _unitOfWork.companyData.GetByIdAsync(dto.CompanyID);

            if (company == null)
            {
                return new BadRequestObjectResult("Invalid company ID.");
            }

            var user = new User
            {
                UserName = dto.Email,

                Email = dto.Email,

                CompanyID = company.Id,

                CompanyName = company.CompanyName,

                reportToMail = dto.ReportToMail,

                rLeav = dto.RLeav
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            var createdUser = await _userManager.FindByEmailAsync(dto.Email);

            await _roleService.AssignRoleToUserAsync(createdUser, dto.Role);

            return new OkObjectResult("User registered successfully.");
        }
    }
}
