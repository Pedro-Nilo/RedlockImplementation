using MongoDB.Bson;

namespace Redlock.Domain.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    Task<T> GetByIdAsync(ObjectId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task InsertAsync(T entity);
    Task<bool> ReplaceOneAsync(T entity);
    Task<bool> DeleteOneAsync(ObjectId id);
}
