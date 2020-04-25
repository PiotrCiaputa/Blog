using Blog.Models;
using Blog.Services;
using Blog.Services.FileManager;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

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
            var articles = _articleRepository.GetAllArticles();
            return View(articles);
        }

        //Artykuły

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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new ArticleViewModel());
            }

            var article = _articleRepository.GetArticle(id);

            return View(new ArticleViewModel
            {
                ID = article.ID,
                Title = article.Title,
                Body = article.Body,
                Description = article.Description,
                Tags = article.Tags,
                Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem { Text = c.Name, Value = c.ID.ToString() }).ToList(),
                CategoryID = article.CategoryID,
                CurrentImage = article.Image
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleViewModel model)
        {
            var article = new Article
            {
                ID = model.ID,
                Title = model.Title,
                Body = model.Body,
                Description = model.Description,
                Tags = model.Tags,
                Created = model.Created,
                CategoryID = model.CategoryID,
                Category = _categoryRepository.GetCategory(model.CategoryID)
            };

            if (model.Image == null)
            {
                article.Image = model.CurrentImage;
            }
            else
            {
                article.Image = await _fileManager.SaveImage(model.Image);
            }

            if (ModelState.IsValid)
            {
                _articleRepository.UpdateArticle(article);
                await _articleRepository.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(article);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var article = _articleRepository.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {           
            _articleRepository.RemoveArticle(id);
            await _articleRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Kategorie
        public IActionResult List()
        {
            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var category = new Category();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(category);
                await _categoryRepository.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {          
            if(id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int? id, Category category)
        {
            if(id == null)
            {
                return NotFound();
            }

            if(id != category.ID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(category);
                await _categoryRepository.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(category);                  
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {        
            var category = _categoryRepository.GetCategory(id);

            if(category == null)
            {
                return NotFound();
            }

            return View(category);       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _categoryRepository.RemoveCategory(id);
            await _categoryRepository.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}