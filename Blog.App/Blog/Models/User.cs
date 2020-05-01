using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog.Models
{
    public class User : IdentityUser
    {
        public List<Comment> Comments { get; set; }
    }
}
