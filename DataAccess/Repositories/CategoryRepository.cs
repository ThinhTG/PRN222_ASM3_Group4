using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;

namespace DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly eStoreContext _context;

    public CategoryRepository(eStoreContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public Category GetById(int id)
    {
        return _context.Categories.FirstOrDefault(p => p.CategoryId == id);
    }

    public void Add(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        var existingCategory = _context.Categories.Find(category.CategoryId);
        if (existingCategory == null)
            throw new ArgumentException("Category not found");

        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
            throw new ArgumentException("Category not found");

        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    public IEnumerable<Category> Search(string categoryName)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(categoryName))
        {
            var searchNameLower = categoryName.ToLower();
            query = query.Where(p => p.CategoryName.ToLower().Contains(searchNameLower));
        }

        return query.ToList();
    }
}