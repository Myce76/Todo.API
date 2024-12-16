using Microsoft.AspNetCore.OData.Query;
using System.Linq.Expressions;

namespace Todo.API.Extensions
{
    public static class FilterQueryOptionExtensions
    {
        public static Expression ToExpression<T>(this FilterQueryOption filterQueryOption) where T : class
        {
            IQueryable queryable = Enumerable.Empty<T>().AsQueryable();
            queryable = filterQueryOption.ApplyTo(queryable, new ODataQuerySettings());

            return queryable.Expression;
        }
    }
}
