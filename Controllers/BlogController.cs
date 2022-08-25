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
            var model = new PostsListModel();
            var skip = _page * 7;
            var select = 7;
            await model.queryPosts(skip, select);

            int nextPage = 0;
            if(model.DBSize > skip + select)
            {
                nextPage = _page + 1;
            }
            ViewBag.Next = nextPage;

            int prevPage = _page - 1;
            ViewBag.Previous = prevPage;

            return View(model);
        }

        [HttpGet]
       

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
