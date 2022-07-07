using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;


namespace Blog.Models
{
    public class PostsListModel
    {
        public List<(Post, int)> GetPosts { get; private set; }
        public int FirstId { get; set; }
        public int ListSize = 10;
        public int Size { get; private set; }

        public async Task<bool> queryPosts()
        {
            this.GetPosts = new List<(Post, int)>();
            bool status = false;
            try
            {
                using (DataBaseContext dbContext = new DataBaseContext())
                {
                    var posts = await dbContext.Posts.OrderByDescending(b => b.Date).Skip(FirstId).Take(ListSize).ToListAsync();
                    var _size = await dbContext.Posts.CountAsync();
                    if(posts!= null || posts.Count != 0)
                    {
                        foreach(Post p in posts)
                        {
                            try
                            {
                                int _commentsCount;
                                _commentsCount = await dbContext.Comments.Where(c => c.PostId == p.Id).Where(c => c.Enable == true).CountAsync();
                                this.GetPosts.Add((p, _commentsCount));
                                this.Size = _size;
                            }
                            catch
                            {
                                this.GetPosts.Add((p, 0));
                            }
                           
                        }

                        status = true;

                    }
                }
            }
            catch
            {
                this.GetPosts = new List<(Post, int)>();
                status = false;
            }

            return status;
            
        }

    }
}
