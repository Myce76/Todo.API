using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> Todos { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<TodoItem>()
                .ToContainer("Todos");

            modelBuilder.Entity<TodoItem>(entity =>
            { 
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Status)
                      .HasConversion(new EnumToStringConverter<ItemStatus>());
            });
        }
    }
}
