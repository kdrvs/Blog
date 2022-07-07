using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class CommentsOnPostModel
    {
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }

        public async Task<bool> commentsQuery()
        {
            var status = false;
            try
            {
                using (DataBaseContext dbContext = new DataBaseContext())
                {
                    var _comments = await dbContext.Comments.Where(c => c.PostId == this.PostId).Where(c => c.Enable == true).ToListAsync();
                    this.Comments = _comments;
                    status = true;
                }
            }
            catch
            {
                status = false;
                Comments = new List<Comment>();
            }
            return status;
            
        }
    }
}
