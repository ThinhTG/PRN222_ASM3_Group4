namespace DataAccess.InterfaceRepo;

public interface IGenericRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}