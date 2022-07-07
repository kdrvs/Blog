using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{ 
    public class NewCommentModel
    {
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string CommentContent { get; set; }

        public async Task<int> saveComment()
        {
            var date = DateTime.Now;
            int commentId = -1;
            using(DataBaseContext dbContext = new DataBaseContext())
            {
                dbContext.Add(new Comment()
                {
                    PostId = this.PostId,
                    Date = date,
                    AutorName = this.AuthorName,
                    Text = this.CommentContent,
                    Enable = !Settings.Pre_moderation_comments
                });

                await dbContext.SaveChangesAsync();

                var comment = await dbContext.Comments
                    .Where(c => c.Text == this.CommentContent && c.Date == date && c.AutorName == this.AuthorName)
                    .FirstOrDefaultAsync();

                commentId = comment.Id;
            }
            return commentId;
        }
    }
}
