using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class ConsolePostsListModel
    {
        public List<Post> Posts { get; private set; }

        public async Task<bool> queryPosts()
        {
            var res = false;
            try
            {
                using (DataBaseContext dbContext = new DataBaseContext())
                {
                    this.Posts = await dbContext.Posts.OrderByDescending(p => p.Date).ToListAsync();
                    res = true;
                }
            }
            catch
            {
                res = false;
                this.Posts = new List<Post>();
            }

            return res;
            
        }

    }
}
