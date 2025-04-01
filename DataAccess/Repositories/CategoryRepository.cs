using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly eStoreContext _context;

    public CategoryRepository(eStoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
    }

    public async Task Add(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
        if (existingCategory == null)
            throw new ArgumentException("Category not found");

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new ArgumentException("Category not found");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> Search(string categoryName)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(categoryName))
        {
            var searchNameLower = categoryName.ToLower();
            query = query.Where(p => p.CategoryName.ToLower().Contains(searchNameLower));
        }

        return await query.ToListAsync();
    }
}
