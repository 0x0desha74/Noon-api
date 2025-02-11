using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities;
using Noon.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria);
            


            if(spec.IsPaginationEnabled)
            {
                query.Skip(spec.Skip);
                query.Take(spec.Take);
            }


            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);


            if(spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);


            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return query;
        }
    }
}
