using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class EntryModel
    {
        public bool Exist { get; private set; }
        public int Id { get; private set; }
        public DateTime dateTime { get; private set; }
        public string Topic { get; private set; }
        public string Content { get; private set; }

        public int CommentsCount { get; private set; }

        public EntryModel()
        {
            
        }

       

        public async Task<bool> QueryPostFromDB(int id)
        {
            var status = false;
            try
            {
                using(DataBaseContext dbContext = new DataBaseContext())
                {
                    var post = await dbContext.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
                    if(post == null)
                    {
                        return false;
                    }
                    this.Id = post.Id;
                    this.dateTime = post.Date;
                    this.Topic = post.Topic;
                    this.Content = post.Text;
                    status = true;

                    try
                    {
                        var _commentsCount = await dbContext.Comments.Where(c => c.PostId == post.Id).Where(c => c.Enable == true).CountAsync();
                        this.CommentsCount = _commentsCount;
                    }
                    catch
                    {
                        this.CommentsCount = 0;
                    }
                    
                }
            }
            catch
            {
                status = false;
            }
            return status;
        }
       
    }
}
