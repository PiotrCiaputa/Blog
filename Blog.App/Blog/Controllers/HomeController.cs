using Blog.Models;
using Blog.Services;
using Blog.Services.FileManager;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

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
        public IActionResult Index(int category, int? page, string search)
        {
            var pageNumber = page ?? 1;

            HomeViewModel model;

            if (category == 0)
            {

                model = new HomeViewModel()
                {
                    Articles = _articleRepository.GetAllArticles(search).ToPagedList(pageNumber, 3),
                    AllArticles = _articleRepository.GetAllArticles(),
                    Categories = _categoryRepository.GetAllCategories()
                };
            }            
            else
            {
                model = new HomeViewModel()
                {
                    Articles = _articleRepository.GetAllArticles(category).ToPagedList(pageNumber, 3),
                    AllArticles = _articleRepository.GetAllArticles(),
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

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Article", new { id = model.ArticleID });
            }

            var article = _articleRepository.GetArticle(model.ArticleID);

            
            article.Comments = article.Comments ?? new List<Comment>();

            article.Comments.Add(new Comment
            {           
                Message = model.Message,                
                Created = DateTime.Now
            });

            _articleRepository.UpdateArticle(article);            
            await _articleRepository.SaveChangesAsync();
            return RedirectToAction("Article", new { id = model.ArticleID });
         }

        [HttpGet]
        public IActionResult About()
        {
            AboutViewModel model = new AboutViewModel
            {
                Articles = _articleRepository.GetAllArticles(),
                Categories = _categoryRepository.GetAllCategories()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ContactViewModel model = new ContactViewModel
            {
                Articles = _articleRepository.GetAllArticles(),
                Categories = _categoryRepository.GetAllCategories()
            };

            return View(model);
        }
    }
}