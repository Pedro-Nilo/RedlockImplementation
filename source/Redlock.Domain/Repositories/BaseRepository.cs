using MongoDB.Bson;
using MongoDB.Driver;
using Redlock.Domain.Interfaces.Repositories;
using Redlock.Domain.Extensions;

namespace Redlock.Domain.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
{
    private readonly IClientSessionHandle _clientSessionHandle;

    public IMongoCollection<T> Collection { get; private set; }
    public bool IsDisposed { get; private set; }


    public BaseRepository(
        IClientSessionHandle clientSessionHandle,
        IMongoClient mongoClient,
        string databaseName,
        string collectionName)
    {
        _clientSessionHandle = clientSessionHandle ?? throw new ArgumentNullException(nameof(clientSessionHandle));
        Collection = mongoClient.GetDatabase(databaseName).GetCollection<T>(collectionName);
    }

    public virtual async Task<T> GetByIdAsync(ObjectId id)
    {
        var filterDefinition = Builders<T>.Filter.Eq(e => e.GetPropertyValue("Id"), id);

        return await Collection.Find(filterDefinition).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var filterDefinition = Builders<T>.Filter.Empty;

        return await Collection.Find(filterDefinition).ToListAsync();
    }

    public virtual async Task InsertAsync(T entity) => await Collection.InsertOneAsync(entity);

    public virtual async Task<bool> ReplaceOneAsync(T entity)
    {
        entity.SetPropertyValue("UpdatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        var id = entity.GetPropertyValue("Id");
        var filterDefinition = Builders<T>.Filter.Eq(e => e.GetPropertyValue("Id"), id);
        var replaceOptions = new ReplaceOptions { IsUpsert = false };
        var replaceOneResult = await Collection.ReplaceOneAsync(filterDefinition, entity, replaceOptions);

        return replaceOneResult.IsAcknowledged && replaceOneResult.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteOneAsync(ObjectId id)
    {
        var filterDefinition = Builders<T>.Filter.Eq(e => e.GetPropertyValue("Id"), id);
        var deleteResult = await Collection.DeleteOneAsync(filterDefinition);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            _clientSessionHandle?.Dispose();
        }

        IsDisposed = true;
    }
}
