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

        Task<IEnumerable<T>> GetAllWithSpec(ISpecifications<T> specifications);
         Task<T>GetByIdWithSpec(ISpecifications<T> specifications);
    }

}
