using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Todo.API.Filters
{
    /// <summary>
    /// Defined custom OperationFilter, which decorates the odata-related endpoints having the EnableQuery attribute
    /// </summary>
    public class ODataOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Decorate OData endpoints with the proper query parameters having the EnableQuery attribute
        /// </summary>
        /// <param name="operation">OpenApi operation</param>
        /// <param name="context">OpenApi filter context</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            if (context.ApiDescription.CustomAttributes().Any(x => x is ODataOperationFilterCustomAttribute))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$filter",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    },
                    Description = "Filter the response with OData filter queries.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$top",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                    },
                    Description = "Number of objects to return. (default. 25)",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$skip",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                    },
                    Description = "Number of objects to skip in the current order (ex. 50)",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$orderby",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    },
                    Description = "Define the order by one or more fields (ex. Description)",
                    Required = false
                });
            }
        }
    }
}
