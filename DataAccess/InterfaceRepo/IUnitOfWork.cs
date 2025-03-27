namespace DataAccess.InterfaceRepo;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    void Save();
    Task SaveAsync();
    bool HasActiveTransaction();
}