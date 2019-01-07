using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Seeds the user roles
        public void Seed()
        {
            CreateRole("Member");
            CreateRole("Customer");
        }

        // Creates a role with name "roleName"
        public async void CreateRole(string roleName)
        {
            if ((await _roleManager.FindByNameAsync(roleName)) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }
    }
}
