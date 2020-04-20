using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class ArticleViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }        
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IFormFile Image { get; set; } = null;
    }
}
