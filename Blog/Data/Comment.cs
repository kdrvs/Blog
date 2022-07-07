using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public string AutorName { get; set; }
        // to do Ip address
        public string Text { get; set; }
        public bool Enable { get; set; }
    }
}
