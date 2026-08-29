namespace Todo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Infrastructure.Configurations;

public class TodoDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new TarefaConfiguration());
    }
}