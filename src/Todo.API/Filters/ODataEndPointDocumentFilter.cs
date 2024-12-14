using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Todo.API.Filters;

/// <summary>
/// Defined custom DocumentFilter, which filters the odata-related irrelevant endpoints
/// </summary>
internal sealed class ODataEndpointDocumentFilter : IDocumentFilter
{
    private readonly IEnumerable<string> _filterableEndpoints = new List<string> { "/$count", "/$metadata" };

    /// <summary>
    /// Apply the defined filtering to the document paths.
    /// </summary>
    /// <param name="swaggerDoc">OpenApi Document</param>
    /// <param name="context">Document filter context</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) => swaggerDoc.Paths
            .Where(path => IsODataEndpoint(path) && _filterableEndpoints.Any(path.Key.Contains))
            .ToList()
            .ForEach(path => swaggerDoc.Paths.Remove(path.Key));

    private static bool IsODataEndpoint(KeyValuePair<string, OpenApiPathItem> path)
    {
        return path.Key.StartsWith("/odata", StringComparison.InvariantCultureIgnoreCase);
    }
}
public class ODataEndPointDocumentFilter
{

}

