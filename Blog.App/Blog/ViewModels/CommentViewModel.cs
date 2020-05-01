using Blog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Message { get; set; }       

        public int ArticleID { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
