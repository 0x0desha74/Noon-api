using Noon.Core.Entities;
using Noon.Core.Repositories;

namespace Noon.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
