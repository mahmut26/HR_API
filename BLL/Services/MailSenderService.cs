using BAL.Database.DatabaseIdentity;
using BAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MailSenderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;

        private readonly RoleService _roleService;

        private readonly EmailService _emailService;

        public MailSenderService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleService roleService, EmailService emailService)
        {
            _unitOfWork = unitOfWork;

            _userManager = userManager;

            _roleService = roleService;

            _emailService = emailService;
        }
        public async Task MailSenderByID(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userRto = await _userManager.FindByIdAsync(user.reportToMail);

            var mail = userRto.Email;


            await _emailService.SendEmailAsync(mail, "Confirm your email", "fasdfasdfa");
        }
    }
}
