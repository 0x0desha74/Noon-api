using Noon.Core.Entities;
using Noon.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> specifications);
        Task<T> GetByIdWithSpec(ISpecifications<T> specifications);
        Task<int> GetCountWithSpecAsync(ISpecifications<T> specifications);
    }

}
