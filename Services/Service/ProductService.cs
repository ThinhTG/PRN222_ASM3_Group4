using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Services.InterfaceService;

namespace Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Add(product);
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Update(product);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> Search(string productName, decimal? unitPrice)
        {
            return _productRepository.Search(productName, unitPrice);
        }
        
        public async Task<Dictionary<int, string>> GetProductsByIds(IEnumerable<int> ids)
        {
            var products = await _productRepository.GetByIds(ids);
            return products.ToDictionary(p => p.ProductId, p => p.ProductName ?? "Unknown Product");
        }
        
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}