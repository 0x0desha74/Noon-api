using Noon.Core.Entities;
using Noon.Core.Specifications;

namespace Noon.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> specifications);
        Task<T> GetEntityWithSpec(ISpecifications<T> specifications);
        Task<int> GetCountWithSpecAsync(ISpecifications<T> specifications);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);




    }

}
