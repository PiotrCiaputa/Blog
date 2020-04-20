﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Services;
using Blog.Services.FileManager;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IArticleRepository _articleRepository;
        private ICategoryRepository _categoryRepository;
        private IFileManager _fileManager;
        public PanelController(IArticleRepository articleRepository,
                              ICategoryRepository categoryRepository,
                              IFileManager fileManager)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var articles = _articleRepository.GetAllArticles().ToList();
            return View(articles);
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
        public async Task<IActionResult> Add(ArticleViewModel model)
        {           
            var article = new Article
            {
                ID = model.ID,
                Title = model.Title,
                Body = model.Body,
                Description = model.Description,
                Tags = model.Tags,
                Created = model.Created,
                Image = await _fileManager.SaveImage(model.Image),
                CategoryID = model.CategoryID,
                Category = _categoryRepository.GetCategory(model.CategoryID)
            };

            if (ModelState.IsValid)
            {
                _articleRepository.AddArticle(article);
                await _articleRepository.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(article);            
        }

        public async Task<IActionResult> Remove(int id)
        {
            _articleRepository.RemoveArticle(id);
            await _articleRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}