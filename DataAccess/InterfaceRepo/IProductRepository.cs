using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(int id);
    Task<IEnumerable<Product>> Search(string productName, decimal? unitPrice);
    Task<IEnumerable<Product>> GetByIds(IEnumerable<int> ids);
    Task<IEnumerable<Product>> GetAllProducts();
}