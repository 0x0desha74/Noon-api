﻿using Noon.Core.Entities;
using Noon.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
