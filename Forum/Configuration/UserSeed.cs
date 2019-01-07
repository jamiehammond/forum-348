using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Configuration
{
    public class UserSeed
    {
        private readonly UserManager<ForumUser> _userManager;

        public UserSeed(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }

        public void Seed()
        {

        }

        public async void CreateUser(string email, string password)
        {
            var user = new ForumUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            _userManager.AddClaimAsync(user, new Claim(")
        }
    }
}
