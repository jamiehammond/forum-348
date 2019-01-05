using System;
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

        // GET: Post
        public ActionResult Index()
        {
            var post = _db.Posts.FirstOrDefault(c => c.Id == 9);
            return View(post);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(CreatePostViewModel vm)
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
                return RedirectToAction("Index", "Post");
            }
            return View(vm);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}