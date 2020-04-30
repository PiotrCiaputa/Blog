using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class AboutViewModel
    {    
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}
