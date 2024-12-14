using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Todo.API.Data;

namespace Todo.API.Extensions
{
    public static class ODataExtensions
    {
        public static void RegisterODataServices(this IServiceCollection services)
        {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EnableLowerCamelCase();
            modelBuilder.EnumType<Data.Enums.States>();

            modelBuilder.EntitySet<TodoItem>("ODataTodo");
            //modelBuilder.EntitySet<ReportGroupModel>("ODataReportGroup");

            services.AddControllers().AddOData(
                options => options.Select().Filter().OrderBy().Count().SetMaxTop(25).AddRouteComponents(
                "odata",
                    modelBuilder.GetEdmModel()));
        }
    }
}
