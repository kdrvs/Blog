using System;


namespace Blog.Data
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Date {get;set;}
        public string Topic { get; set; }
        public string Text { get; set; }
    }
}