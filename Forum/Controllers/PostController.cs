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
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            var comments = _db.Comments.Where(c => c.Post == post);
            return View((post, comments));
        }

        // View a list of all posts
        public IActionResult PostList()
        {
            ICollection<Post> posts = _db.Posts.ToList();
            return View(posts);
        }

        // Get method of createComment
        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string content, int postId)
        {
            // If vm state is valid:
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var comment = new Comment()
                {
                    Author = currentUser,
                    AuthorName = currentUser.UserName,
                    Content = content,
                    DatePosted = DateTime.Now,
                    Post = _db.Posts.Find(postId)
                };
                _db.Comments.Add(comment);
                _db.Posts.Find(postId).Comments.Add(comment);
                _db.SaveChanges();
                return RedirectToAction("ViewPost", "Post", postId);
            }
            return View();
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

                // Get current user
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                // Create new post with vm data
                var post = new Post()
                {
                    Author = currentUser,
                    AuthorName = currentUser.UserName,
                    Title = vm.Title,
                    Content = vm.Content,
                    DatePosted = DateTime.Now
                };
                // Add post to context and save changes
                _db.Posts.Add(post);
                _db.SaveChanges();
                // View the newly made post
                return RedirectToAction("ViewPost", "Post", new { postId = post.Id });
            }
            return View(vm);
        }
    }
}