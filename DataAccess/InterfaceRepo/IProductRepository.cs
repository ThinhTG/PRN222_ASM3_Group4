using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface IProductRepository
{
    IEnumerable<Product> GetAll(); 
    Product GetById(int id);
    void Add(Product product); 
    void Update(Product product); 
    void Delete(int id);
    IEnumerable<Product> Search(string productName, decimal? unitPrice);
    Task<IEnumerable<Product>> GetByIds(IEnumerable<int> ids);
    Task<IEnumerable<Product>> GetAllProducts();
}