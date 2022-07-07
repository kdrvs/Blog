using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Data;

namespace Blog.Models
{
    public class ChangeCommentModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public async Task<bool> changeActive()
        {
            bool res = false;
            try
            {
                using(DataBaseContext dbContext = new DataBaseContext())
                {
                    var comment = await dbContext.Comments.FindAsync(this.Id);
                    comment.Enable = !this.Status;
                    await dbContext.SaveChangesAsync();
                    res = true;
                }
            }
            catch
            {
                res = false;
            }

            return res;
        }
    }
}
