using Blog.Data;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private AppDbContext _context;
        public void AddArticle(Article article)
        {
            _context.Articles.Add(article);
        }

        public List<Article> GetAllArticles()
        {
            return _context.Articles.ToList();
        }

        public Article GetArticle(int id)
        {
            return _context.Articles.Single(x => x.ID == id);
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
