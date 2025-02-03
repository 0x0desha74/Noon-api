using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Core.Specifications;
using Noon.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<T>> GetAllWithSpec(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        

        public async Task<T> GetByIdWithSpec(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.BuildQuery(_dbContext.Set<T>(), spec);
        }

      
    }
}
