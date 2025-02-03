using Noon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


        public BaseSpecification() //for no filtering => no criteria
        {

        }


        public BaseSpecification(Expression<Func<T, bool>>? criteria ) //with filtering => with criteria
        {
            Criteria = criteria;
        }
    }
}
