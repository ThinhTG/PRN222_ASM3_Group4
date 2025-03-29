using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category GetById(int id);
    void Add(Category category);
    void Update(Category category);
    void Delete(int id);
    IEnumerable<Category> Search(string categoryName);
}