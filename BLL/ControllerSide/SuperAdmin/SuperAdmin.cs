using BAL.Database.Company;
using BAL.Database.DatabaseIdentity;
using BAL.Database.RequestCompany;
using BAL.UnitOfWork;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ControllerSide.SuperAdmin
{
    public class SuperAdmin
    {
        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        private readonly RoleService _roleService;

        public SuperAdmin(UserManager<User> userManager, IUnitOfWork unitOfWork, RoleService roleService)
        {
            _userManager = userManager;

            _unitOfWork = unitOfWork;

            _roleService = roleService;
        }

        public async Task<string> RegisterUser(string email, string password, string companyName)
        {
            await _unitOfWork.companyData.AddAsync(new CompanyData { CompanyName = companyName });

            await _unitOfWork.SaveAsync();

            var company = (await _unitOfWork.companyData.GetAllAsync()).FirstOrDefault(y => y.CompanyName == companyName);

            if (company == null)
                return "Company creation failed.";

            var user = new User
            {
                UserName = email,

                Email = email,

                CompanyID = company.Id,

                CompanyName = companyName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return "User creation failed.";

            var newCompany = await _unitOfWork.reqComp.GetAllAsync();

            var toBeUpdated = newCompany.FirstOrDefault(x => x.CompanyName == companyName);

            if (toBeUpdated != null)
            {
                toBeUpdated.Created = true;

                await _unitOfWork.reqComp.UpdateAsync(toBeUpdated);

                await _unitOfWork.SaveAsync();
            }

            var createdUser = await _userManager.FindByEmailAsync(email);

            await _roleService.AssignRoleToUserAsync(createdUser, "Admin");

            return "User registered successfully.";
        }

        public async Task<IEnumerable<CompanyData>> GetCompanies()
        {
            return await _unitOfWork.companyData.GetAllAsync();
        }

        public async Task<IEnumerable<NewCompanyRequest>> GetCompanyRequests()
        {
            return await _unitOfWork.reqComp.GetAllAsync();
        }

        public async Task<IdentityResult> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
