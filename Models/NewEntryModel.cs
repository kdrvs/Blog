using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class NewEntryModel
    {

        public string Topic { get; set; }
        public string Text { get; set; }

        public async Task<int> SavePost()
        {
            int postId = -1;

            try
            {
                using (DataBaseContext dbContext = new DataBaseContext())
                {
                   
                    await dbContext.AddAsync(new Post()
                    {
                        Topic = this.Topic,
                        Text = this.Text,
                        Date = DateTime.Now
                    });
                    await dbContext.SaveChangesAsync();
                       
                    var _post = await dbContext.Posts.Where(p => p.Topic == this.Topic).FirstOrDefaultAsync();
                    if (_post != null)
                    {
                        postId = _post.Id;
                    }
                   

                }

            }
            catch
            {
                postId = -1;
            }
            return postId;
        }
    }
}
