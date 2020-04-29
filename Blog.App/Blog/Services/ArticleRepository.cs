using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
        
        public Article GetArticle(int? id)
        {
            return _context.Articles.Include(y => y.Comments).FirstOrDefault(x => x.ID == id);
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

        public List<Article> GetAllArticles()
        {
            return _context.Articles.OrderByDescending(x => x.Created).ToList();
        }

        public List<Article> GetAllArticles(int categoryID)
        {
            return _context.Articles.Where(x => x.CategoryID == categoryID)
                                    .OrderByDescending(x => x.Created)
                                    .ToList();
        }

        public List<Article> GetAllArticles(string search)
        {
            var query = _context.Articles.AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(search))

                query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                    || EF.Functions.Like(x.Body, $"%{search}%")
                                    || EF.Functions.Like(x.Description, $"%{search}%")
                                    || EF.Functions.Like(x.Tags, $"%{search}%"));

                return query.OrderByDescending(x => x.Created).ToList();                
        }
    }
}
