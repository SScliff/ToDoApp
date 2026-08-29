namespace Todo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class TodoDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        
    }
}