using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Configuration
{
    public static class DbSeederAlt
    {

        // Seeds the database
        public static void Seed(ForumContext db, UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db.Database.EnsureCreated();

            SeedUsers(userManager);
            SeedRoles(roleManager);
        }

        public static void SeedUsers(UserManager<ForumUser> userManager)
        {
            CreateUser("Member1@email.com", "Password123!", "Member", userManager);
            CreateUser("Customer1@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer2@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer3@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer4@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer5@email.com", "Password123!", "Customer", userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            CreateRole("Member", roleManager);
            CreateRole("Customer", roleManager);
        }

        // Creates a role with name "roleName"
        public static async void CreateRole(string roleName, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole { Name = roleName };
            await roleManager.CreateAsync(role);
        }

        // Creates a user with specified email, password and role
        public static async void CreateUser(string email, string password, string role, UserManager<ForumUser> userManager)
        {
            var user = new ForumUser { UserName = email, Email = email };
            await userManager.CreateAsync(user, password);

            var currentUser = await userManager.FindByNameAsync(user.UserName);
            await userManager.AddToRoleAsync(currentUser, role);

        }
    }
}
