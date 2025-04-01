using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task<IEnumerable<Category>> Search(string categoryName);
    }
}
