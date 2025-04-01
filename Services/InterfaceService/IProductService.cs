using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<IEnumerable<Product>> Search(string productName, decimal? unitPrice);
        Task<Dictionary<int, string>> GetProductsByIds(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
