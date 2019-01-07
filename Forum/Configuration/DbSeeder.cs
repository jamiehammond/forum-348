using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Configuration
{
    public static class DbSeeder
    {
        // Seeds the database
        public static void Seed(ForumContext db, UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db.Database.EnsureCreated();

            SeedRoles(roleManager);
            SeedUsers(userManager);
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
        public static void CreateRole(string roleName, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole { Name = roleName };
            roleManager.CreateAsync(role).Wait();
        }

        // Creates a user with specified email, password and role
        public static void CreateUser(string email, string password, string role, UserManager<ForumUser> userManager)
        {
            var user = new ForumUser { UserName = email, Email = email };
            userManager.CreateAsync(user, password).Wait();

            var currentUser = userManager.FindByNameAsync(user.UserName).Result;
            if (currentUser == null)
            {
            } else
            {
                userManager.AddToRoleAsync(currentUser, role).Wait();
            }
        }
    }
}
