using Redlock.Domain.Entities;

namespace Redlock.Domain.Interfaces.Repositories;

public interface IResourceRepository : IBaseRepository<Resource>
{
    Task<Resource> GetByNotInUseAsync();
}
