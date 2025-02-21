using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        private Hashtable? _repositories;
        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }



        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
                _repositories.Add(type, new GenericRepository<TEntity>(_dbContext));

            return _repositories[type] as IGenericRepository<TEntity>;
        }



        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
             => _dbContext.Dispose();
    }
}
