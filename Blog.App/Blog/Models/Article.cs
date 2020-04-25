﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
