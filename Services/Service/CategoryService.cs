using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using DataAccess.Repositories;
using Microsoft.AspNetCore.SignalR;
using Services.HubSignalR;
using Services.InterfaceService;

namespace Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHubContext<Hubs> _hubContext;
        public CategoryService(ICategoryRepository categoryRepository,  IHubContext<Hubs> hubContext)
        {
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
        }

        public async Task Add(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            await _categoryRepository.Add(category);
            await _hubContext.Clients.All.SendAsync("Created");
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            await _categoryRepository.Delete(id);

            if (category != null)
            {
                await _hubContext.Clients.All.SendAsync("Deleted");
            }
        }
        

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task<IEnumerable<Category>> Search(string categoryName)
        {
            return await _categoryRepository.Search(categoryName);
        }

        public async Task Update(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            await _categoryRepository.Update(category);
            await _hubContext.Clients.All.SendAsync("Updated");
        }
    }
}
