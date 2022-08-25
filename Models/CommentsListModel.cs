using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class CommentsListModel
    {
        public List<CommentData> Comments { get; private set; }

        public async Task<bool> queryComments()
        {
            var status = false;
            try
            {
                using(DataBaseContext dbContext = new DataBaseContext())
                {
                    var _comments = await (from com in dbContext.Comments
                              orderby com.Date descending
                              join post in dbContext.Posts on com.PostId equals post.Id
                              select new CommentData()
                              {
                                  PostId = post.Id,
                                  PostTopic = post.Topic,
                                  CommentId = com.Id,
                                  CommentDate = com.Date,
                                  CommentAutorName = com.AutorName,
                                  CommentText = com.Text,
                                  CommentEnable = com.Enable
                              }).ToListAsync();
                    this.Comments = _comments;
                    status = true;
                }
            }
            catch
            {
                status = false;
                this.Comments = new List<CommentData>();
            }
            return status;
        }

        
    }

    public class CommentData
    {
        public int PostId;
        public string PostTopic;
        public int CommentId;
        public DateTime CommentDate;
        public string CommentAutorName;
        public string CommentText;
        public bool CommentEnable;
    }
}
