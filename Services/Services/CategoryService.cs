using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public CategoryService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;            
        }

        public async Task<Category> CreateCategory(Category category, string userID)
        {
            category.UserId = userID;
            await _ReadLaterDataContext.AddAsync(category);
            await _ReadLaterDataContext.SaveChangesAsync();
            return  category;
        }

        public async Task<bool> UpdateCategory(Category category, string userID)
        {
            try
            {
                category.UserId = userID;
                _ReadLaterDataContext.Update(category);
                await _ReadLaterDataContext.SaveChangesAsync();
                return true;
            }catch (Exception ex) 
            {
                return false; 
            }       
        }
        public async Task<Category> CreateCategory(string categoryName,string userID)
        {
            var existingCategory = await _ReadLaterDataContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);

            if (existingCategory != null)
            {
                return existingCategory; 
            }

            var newCategory = new Category
            {
                Name = categoryName,             
            };
            newCategory.UserId = userID;

            _ReadLaterDataContext.Categories.Add(newCategory);
            await _ReadLaterDataContext.SaveChangesAsync();

            return newCategory;
        }
        public async Task<List<Category>> GetCategories(string userId)
        {
            return await _ReadLaterDataContext.Categories.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetCategory(int Id)
        {
            return await _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategory(string Name)
        {
            return await _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            try
            {
                _ReadLaterDataContext.Categories.Remove(category);
                await _ReadLaterDataContext.SaveChangesAsync();
                return true;
            } catch (Exception ex) { return false; }
        }
    }
}
