using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string page)
        {
            int _page;
            int.TryParse(page, out _page);
            if (_page < 0) _page = 0;
            var model = new PostsListModel();
            model.FirstId = _page;
            await model.queryPosts();
            return View(model);
        }

        
        public async Task<IActionResult> Next(string id)
        {
            int _page;
            int.TryParse(id, out _page);
            if (_page < 0) _page = 0;
            var model = new PostsListModel();
            model.FirstId = _page;
            await model.queryPosts();
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Entry(string id)
        {
            int postId;
            var entry = new EntryModel();

            if (!int.TryParse(id, out postId))
            {
                return RedirectToAction("Index"); //to do public Error page
            }

            if(!await entry.QueryPostFromDB(postId))
            {
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
