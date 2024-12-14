
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Todo.API.Extensions;
using Todo.API.Filters;

namespace Todo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.RegisterODataServices();
            builder.Services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<ODataEndpointDocumentFilter>();
                c.OperationFilter<ODataOperationFilter>();
                c.SwaggerDoc($"v1", new OpenApiInfo
                {
                    Title = "Todo Api",
                    Description = "Todo Api for test",
                    Version = "v1",
                });
                c.EnableAnnotations();
                
                var xmlFiles = new[]
                {
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
                };

                foreach (var xmlFile in xmlFiles)
                {
                    var xmlCommentFile = xmlFile;
                    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                    if (File.Exists(xmlCommentsFullPath))
                    {
                        c.IncludeXmlComments(xmlCommentsFullPath);
                    }
                }
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
