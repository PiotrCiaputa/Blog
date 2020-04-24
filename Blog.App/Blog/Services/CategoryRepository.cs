using Blog.Data;
using Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.ID == id);
        }        

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        public void RemoveCategory(int id)
        {
            _context.Categories.Remove(GetCategory(id));
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
