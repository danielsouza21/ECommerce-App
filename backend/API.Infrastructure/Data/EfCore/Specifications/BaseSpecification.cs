using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using API.Core.Specifications;

namespace API.Infrastructure.Data.EfCore.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();  //Default empty list
        public Expression<Func<T, object>> OrderBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Expression<Func<T, object>> OrderByDescending { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected void AddInclude(Expression<Func<T, object>> includedExpression)
        {
            Includes.Add(includedExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
