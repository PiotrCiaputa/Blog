using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Services;
using Blog.Services.FileManager;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Index()
        {
            var articles = _articleRepository.GetAllArticles();
            return View(articles);
        }

        public IActionResult Article(int id)
        {
            var article = _articleRepository.GetArticle(id);
            return View(article);
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.StreamImage(image), $"image/{mime}");
        }
    }
}