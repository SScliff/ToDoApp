using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Infrastructure.Configurations;
namespace Todo.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Category> Categories { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
    }
}
