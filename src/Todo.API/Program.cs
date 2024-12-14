
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text.Json.Serialization;
using Todo.API.Extensions;
using Todo.API.Filters;
using Todo.Infrastructure.Persistence;

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
            builder.Services.AddDbContext<TodoContext>(options =>
                                                       options.UseCosmos(builder.Configuration.GetConnectionString("DefaultConnection") ??
                                                       "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                                                       "TodosDB"));
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

            using (var scopeAsync = app.Services.CreateAsyncScope())
            {
                var dbContext = scopeAsync.ServiceProvider.GetRequiredService<TodoContext>();
                Task.Run(async () =>
                {
                    await dbContext.Database.EnsureDeletedAsync();
                    await dbContext.Database.EnsureCreatedAsync();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
