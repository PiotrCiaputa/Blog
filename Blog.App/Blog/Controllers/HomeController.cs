using Blog.Services;
using Blog.Services.FileManager;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileManager _fileManager;
        
        public HomeController(IArticleRepository articleRepository,
                              ICategoryRepository categoryRepository,
                              IFileManager fileManager)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _fileManager = fileManager;
        }
        public IActionResult Index(int category)
        {
            HomeViewModel model;
            if(category == 0)
            {
                model = new HomeViewModel()
                {
                    Articles = _articleRepository.GetAllArticles(),
                    Categories = _categoryRepository.GetAllCategories()
                };
            }
            else
            {
                model = new HomeViewModel()
                {
                    Articles = _articleRepository.GetAllArticles(category),
                    Categories = _categoryRepository.GetAllCategories()
                };
            }           
            
            return View(model);
        }

        public IActionResult Article(int id)
        {
            var model = new OneArticleViewModel()
            {
                Article = _articleRepository.GetArticle(id),
                Categories = _categoryRepository.GetAllCategories(),
                Articles = _articleRepository.GetAllArticles()
            };
           
            return View(model);
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.StreamImage(image), $"image/{mime}");
        }
    }
}