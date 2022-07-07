using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;


namespace Blog.Models
{
    public class EditEntryModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public async Task<bool> queryPostById(int _id)
        {
            var res = false;
            try
            {
                using(DataBaseContext dbContext = new DataBaseContext())
                {
                    var post = await dbContext.Posts.FindAsync(_id);
                    this.Id = post.Id;
                    this.Date = post.Date;
                    this.Title = post.Topic;
                    this.Text = post.Text;
                    res = true;
                }
            }
            catch
            {
                res = false;

            }

            return res;
        }

        public async Task<bool> saveChange()
        {
            var res = false;
            try
            {
                using (DataBaseContext dbContext = new DataBaseContext())
                {
                    var post = await dbContext.Posts.FindAsync(this.Id);
                    post.Date = this.Date;
                    post.Topic = this.Title;
                    post.Text = this.Text;

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

        public async Task<bool> deleteItem()
        {
            var res = false;
            try
            {
                using(DataBaseContext dbContext = new DataBaseContext())
                {
                    dbContext.Posts.Remove(await dbContext.Posts.FindAsync(this.Id));
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
