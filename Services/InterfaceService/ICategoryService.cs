using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        IEnumerable<Category> Search(string categoryName);
    }
}
