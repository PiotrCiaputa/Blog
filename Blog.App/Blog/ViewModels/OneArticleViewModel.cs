using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class OneArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Article> Articles { get; set; }        
    }
}
