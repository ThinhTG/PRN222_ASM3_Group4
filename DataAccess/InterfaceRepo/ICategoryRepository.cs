using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(int id);
    Task Add(Category category);
    Task Update(Category category);
    Task Delete(int id);
    Task<IEnumerable<Category>> Search(string categoryName);
}