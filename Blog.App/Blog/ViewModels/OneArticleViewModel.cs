using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class OneArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Article> Articles { get; set; }        
    }
}
