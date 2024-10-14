using BAL.Database.DatabaseIdentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService
    {
        private readonly UserManager<User> _userManager;

        public RoleService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssignRoleToUserAsync(User user, string roleName)
        {
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
        public async Task RemoveRoleFromUserAsync(User user, string roleName)
        {
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
        }
    }
}
