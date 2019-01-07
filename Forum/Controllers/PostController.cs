using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    // Post controller - for viewing, creating and commenting on posts
    // Must be a member or customer to access posts
    [Authorize(Roles = "Member, Customer")]
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

        // View the post of Id postId
        public IActionResult ViewPost(int postId)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            ICollection<Comment> comments = _db.Comments.Where(c => c.Post == post).ToList();
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

        // Post method for createComment - creates a comment on a given post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string content, int postId)
        {
            // If vm state is valid:
            if (ModelState.IsValid)
            {
                // Gets the currently logged in user
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                // Creates a new comment with the given parameters
                var comment = new Comment()
                {
                    AuthorName = currentUser.UserName,
                    Content = content,
                    DatePosted = DateTime.Now,
                    Post = _db.Posts.Find(postId)
                };

                // Add the comment to the database and post
                _db.Comments.Add(comment);
                _db.Posts.Find(postId).Comments.Add(comment);
                _db.SaveChanges();

                // Return to the original post that was commented on
                return RedirectToAction("ViewPost", "Post", new { postId });
            }
            // If invalid, return to the post
            return RedirectToAction("ViewPost", "Post", new { postId });
        }

        // Get method for createPost
        [HttpGet]
        [Authorize(Roles = "Member")]
        public IActionResult CreatePost()
        {
            return View();
        }

        // Post method to create a new post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CreatePost(CreatePostViewModel vm)
        {

            // If vm state is valid:
            if (ModelState.IsValid)
            {

                // Get current user
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                // Create new post with vm data
                var post = new Post()
                {
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