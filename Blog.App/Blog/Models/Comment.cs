using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Comment
    {
        public int ID { get; set; }       
        
        public string Message { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int ArticleID { get; set; }
        public Article Article { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Username { get; set; }
    }
}
