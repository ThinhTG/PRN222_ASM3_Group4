using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly eStoreContext _context;

    public ProductRepository(eStoreContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .Include(p => p.Category)
            .ToList();
    }

    public Product GetById(int id)
    {
        return _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);
    }

    public void Add(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        var existingProduct = _context.Products.Find(product.ProductId);
        if (existingProduct == null)
            throw new ArgumentException("Product not found");

        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            throw new ArgumentException("Product not found");

        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public IEnumerable<Product> Search(string productName, decimal? unitPrice)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(productName))
        {
            var searchNameLower = productName.ToLower();
            query = query.Where(p => p.ProductName.ToLower().Contains(searchNameLower));
        }

        if (unitPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice <= unitPrice.Value);
        }

        return query.ToList();
    }
    
    public async Task<IEnumerable<Product>> GetByIds(IEnumerable<int> ids)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.ProductId))
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }
}