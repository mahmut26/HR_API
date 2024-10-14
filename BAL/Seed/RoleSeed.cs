using BAL.Database.DatabaseIdentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Seed
{
    public class RoleSeeder
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleSeeder(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new List<Role>
        {
            new Role { Name = "Admin" },
            new Role { Name = "Personnel" },
            new Role { Name = "Manager" },
            new Role { Name = "SuperAdmin" }
        };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
