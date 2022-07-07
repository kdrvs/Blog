using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class ConsoleController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var model = new ConsolePostsListModel();
            var res = await model.queryPosts();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            int _id;
            if(!int.TryParse(id, out _id))
            {
                return RedirectToAction("All"); //todo to 404
            }
            var model = new EditEntryModel();
            if(!await model.queryPostById(_id))
            {
                return RedirectToAction("All"); //todo to 404
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditEntryModel model)
        {
            if(!await model.saveChange())
            {
                return RedirectToAction("All"); //todo to 404
            }
            return RedirectToAction("Entry", "Blog", new { id = model.Id.ToString() });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Del(string id)
        {
            int _id;
            if(!int.TryParse(id, out _id))
            {
                return RedirectToAction("All"); //todo to 404
            }

            var model = new EditEntryModel();
            if (!await model.queryPostById(_id))
            {
                return RedirectToAction("All"); //todo to 404
            }

            if(!await model.deleteItem())
            {
                return RedirectToAction("All"); //todo to 404
            }
            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult NewPost()
        {
            return View(new NewEntryModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewPost(NewEntryModel entry)
        {
            var postId = await entry.SavePost();
            if(postId < 0)
            {
                return View(entry);
            }
            return RedirectToAction("Entry", "Blog", new { id = postId.ToString()}); //todo
            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Comments()
        {
            ViewBag.PreModeOptions = Settings.Pre_moderation_comments.ToString();
            var model = new CommentsListModel();
            var status = await model.queryComments();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeCommentStatus(ChangeCommentModel model)
        {
            await model.changeActive();
            return RedirectToAction("Comments", "Console", "comment"+model.Id.ToString());
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePreModeOptions()
        {
            Settings.Pre_moderation_comments = !Settings.Pre_moderation_comments;
            return RedirectToAction("Comments");
        }
    }
}
