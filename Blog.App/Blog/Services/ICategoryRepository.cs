using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        Category GetCategory(int id);        
        void UpdateCategory(Category category);
        void RemoveCategory(int id);        

        Task<bool> SaveChangesAsync();
    }
}
