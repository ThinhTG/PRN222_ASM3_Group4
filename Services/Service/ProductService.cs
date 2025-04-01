using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Microsoft.AspNetCore.SignalR;
using Services.HubSignalR;
using Services.InterfaceService;

namespace Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHubContext<Hubs> _hubContext;

        public ProductService(IProductRepository productRepository, IHubContext<Hubs> hubContext)
        {
            _productRepository = productRepository;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _productRepository.Add(product);

            await _hubContext.Clients.All.SendAsync("Update");
        }

        public async Task Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _productRepository.Update(product);

            if (product != null)
            {
                await _hubContext.Clients.All.SendAsync("Update");
            }
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            await _productRepository.Delete(id);

            if (product != null)
            {
                await _hubContext.Clients.All.SendAsync("Update");
            }
        }

        public async Task<IEnumerable<Product>> Search(string productName, decimal? unitPrice)
        {
            return await _productRepository.Search(productName, unitPrice);
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