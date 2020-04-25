using Blog.Data;
using Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddArticle(Article article)
        {
            _context.Articles.Add(article);
        }

        public List<Article> GetAllArticles()
        {
            return _context.Articles.ToList();
        }
        public List<Article> GetAllArticles(int categoryID)
        {
            return _context.Articles.Where(x => x.CategoryID == categoryID).ToList();
        }

        public Article GetArticle(int? id)
        {
            return _context.Articles.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
        }

        public void RemoveArticle(int id)
        {
            _context.Articles.Remove(GetArticle(id));
        }

        public async Task<bool> SaveChangesAsync()
        { 
            if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;           
        }        
    }
}
