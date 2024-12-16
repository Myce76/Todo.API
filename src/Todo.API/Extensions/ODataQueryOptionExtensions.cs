using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.API.Extensions
{
    public static class ODataQueryOptionExtensions
    {
        public static GetRequest<T> ToGetRequest<T>(this ODataQueryOptions<T> request) where T : BaseEntity
        {
            var getRequest = new GetRequest<T>();
            if (request.Filter != null)
            {
                getRequest.Filter = (System.Linq.Expressions.Expression<Func<T, bool>>?)request.Filter.ToExpression<T>();
            }
            if (request.Skip != null)
            {
                getRequest.Skip = request.Skip.Value; 
            }
            if (request.Top != null)
            {
                getRequest.Take = request.Top.Value;
            }

            return getRequest;

        }
    }
}
