using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> AllArticles { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
