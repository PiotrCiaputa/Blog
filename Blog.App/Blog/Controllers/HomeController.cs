using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(IArticleRepository articleRepository,
                              ICategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            //var articles = _articleRepository.GetAllArticles();
            return View();
        }

        public IActionResult Article(int id)
        {
            var article = _articleRepository.GetArticle(id);
            return View(article);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ArticleViewModel articleViewModel =
                new ArticleViewModel();

            articleViewModel.Categories = _categoryRepository.GetAllCategories()
                  .Select(c => new SelectListItem { Text = c.Name, Value = c.ID.ToString() }).ToList();

            return View(articleViewModel);            
        }

        [HttpPost]
        public IActionResult Add(Article article)
        {
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            _articleRepository.RemoveArticle(id);
            await _articleRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}