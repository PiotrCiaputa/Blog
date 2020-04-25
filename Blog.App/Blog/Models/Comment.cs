using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int ArticleID { get; set; }
        public Article Article { get; set; }
    }
}
