using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(Category category, string userId);
        Task<List<Category>> GetCategories(string userID);
        Task<Category> CreateCategory(string categoryName,string userID);
        Task<Category> GetCategory(int Id);
        Task<Category> GetCategory(string Name);
        Task<bool> UpdateCategory(Category category, string userID);
        Task<bool> DeleteCategory(Category category);
    }
}
