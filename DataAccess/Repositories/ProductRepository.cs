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

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task Add(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        var existingProduct = await _context.Products.FindAsync(product.ProductId);
        if (existingProduct == null)
            throw new ArgumentException("Product not found");

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new ArgumentException("Product not found");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> Search(string productName, decimal? unitPrice)
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

        return await query.ToListAsync();
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