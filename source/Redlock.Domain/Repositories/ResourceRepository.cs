using MongoDB.Driver;
using Redlock.Domain.Entities;
using Redlock.Domain.Interfaces.Repositories;

namespace Redlock.Domain.Repositories;

public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
{
    public ResourceRepository(
            IClientSessionHandle sessionHandle,
            IMongoClient client,
            string database,
            string collection) : base(sessionHandle, client, database, collection)
    {
    }

    public async Task<Resource> GetByNotInUseAsync()
    {
        var filterDefinition = Builders<Resource>.Filter.Eq(e => e.InUse, false);

        return await Collection.Find(filterDefinition).FirstOrDefaultAsync();
    }
}
