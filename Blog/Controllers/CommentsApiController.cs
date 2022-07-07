using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Newtonsoft.Json.Linq;


namespace Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsApiController : ControllerBase
    {
        
        private readonly ILogger<CommentsApiController> _logger;
        public CommentsApiController(ILogger<CommentsApiController> logger)
        {
            _logger = logger;
        }

       
        [HttpGet]
        public async Task<CommentsOnPostModel> Get(int postId)
        {
            var model = new CommentsOnPostModel();
            model.PostId = postId;
            await model.commentsQuery();
            return model;
        }

        [HttpPost]
        public async Task<string> Post(NewCommentModel model)
        {
            int commentId = await model.saveComment();

            return commentId.ToString();
        }


    }
}
