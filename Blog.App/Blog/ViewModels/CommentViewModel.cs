using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Message { get; set; }       

        public int ArticleID { get; set; }       
    }
}
