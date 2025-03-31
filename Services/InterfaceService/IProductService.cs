using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        IEnumerable<Product> Search(string productName, decimal? unitPrice);
        Task<Dictionary<int, string>> GetProductsByIds(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
