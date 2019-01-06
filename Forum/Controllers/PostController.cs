using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<ForumUser> _userManager;
        private ForumContext _db;

        // Constructor for class
        public PostController(UserManager<ForumUser> userManager, ForumContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // Index method
        public IActionResult Index()
        {
            return View();
        }

        // View the post of Id postId
        public IActionResult ViewPost(int postId)
        {
            var post = _db.Posts.FirstOrDefault(c => c.Id == postId);
            return View(post);
        }

        // View a list of all posts
        public IActionResult PostList()
        {
            ICollection<Post> posts = _db.Posts.ToList();
            return View(posts);
        }

        // Get method for createPost
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        // Post method to create a new post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreatePostViewModel vm)
        {
            
            // If vm state is valid:
            if (ModelState.IsValid)
            {

                // Create new post
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var post = new Post()
                {
                    Author = currentUser,
                    AuthorName = currentUser.UserName,
                    Title = vm.Title,
                    Content = vm.Content,
                    DatePosted = DateTime.Now
                };
                _db.Posts.Add(post);
                _db.SaveChanges();
                return RedirectToAction("ViewPost", "Post", new { postId = post.Id });
            }
            return View(vm);
        }
    }
}