using Blog.Models;
using Blog.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IArticleRepository
    {
        List<Article> GetAllArticles();        
        List<Article> GetAllArticles(string search);        
        List<Article> GetAllArticles(int categoryID);
        void AddArticle(Article article);
        Article GetArticle(int? id);
        void UpdateArticle(Article article);
        void RemoveArticle(int id);

        Task<bool> SaveChangesAsync();
    }
}
