using Noon.Core.Entities;
using System.Linq.Expressions;

namespace Noon.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification(string? sort, int? brandId, int? typeId) //for no filtering => no criteria
        {

        }


        public BaseSpecification(Expression<Func<T, bool>>? criteria) //with filtering => with criteria
        {
            Criteria = criteria;
        }




        public void AddOrderAscBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderDescBy(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        public void ApplyPagination(int take, int skip)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
