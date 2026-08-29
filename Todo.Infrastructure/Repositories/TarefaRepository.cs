using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Respositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TodoDbContext _context;

    public TarefaRepository(TodoDbContext context)
    {
        _context = context;
    }
    
}