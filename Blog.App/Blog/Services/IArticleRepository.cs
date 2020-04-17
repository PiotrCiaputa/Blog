using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IArticleRepository
    {
        List<Article> GetAllArticles();
        void AddArticle(Article article);
        Article GetArticle(int id);
        void UpdateArticle(Article article);
        void RemoveArticle(int id);

        Task<bool> SaveChangesAsync();
    }
}
