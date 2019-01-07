using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Configuration
{
    // Seeds the database with initial users and roles
    public static class DbSeeder
    {

        // Creates the database if there isn't one, then seeds the roles and users
        public static void Seed(ForumContext db, UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db.Database.EnsureCreated();

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        // Seeds the users with usernames, passwords, and roles
        public static void SeedUsers(UserManager<ForumUser> userManager)
        {
            CreateUser("Member1@email.com", "Password123!", "Member", userManager);
            CreateUser("Customer1@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer2@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer3@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer4@email.com", "Password123!", "Customer", userManager);
            CreateUser("Customer5@email.com", "Password123!", "Customer", userManager);
        }

        // Seeds the roles
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
            userManager.AddToRoleAsync(currentUser, role).Wait();
        }
    }
}
