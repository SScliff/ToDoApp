using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Specifications;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TodoDbContext _context;

    public TarefaRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<Tarefa> CriarTarefaAsync(string titulo, string? descricao, PrioridadeTarefa prioridade, DateTime? prazo)
    {
        var tarefa = new Tarefa(titulo, descricao, prioridade, prazo);
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<Tarefa?> BuscarTarefaPorIdAsync(Guid id)
    {
        return await _context.Tarefas
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task EditarTarefaAsync(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirTarefaAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
        if (tarefa == null)
            return;

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task<(List<Tarefa> Itens, int Total)> ListarTarefasAsync(TarefaFiltro filtro)
    {
        var query = _context.Tarefas.AsQueryable();

        if (filtro.Status.HasValue)
            query = query.Where(t => t.Status == filtro.Status);

        if (filtro.Prioridade.HasValue)
            query = query.Where(t => t.Prioridade == filtro.Prioridade);

        if (filtro.CategoriaId.HasValue)
            query = query.Where(t => t.CategoriaId == filtro.CategoriaId);

        if (!string.IsNullOrWhiteSpace(filtro.Busca))
            query = query.Where(t => t.Titulo.Contains(filtro.Busca) || (t.Descricao != null && t.Descricao.Contains(filtro.Busca)));

        var total = await query.CountAsync();

        var ordenarDesc = filtro.OrdenarPorDirecao.Equals("desc", StringComparison.OrdinalIgnoreCase);
        query = filtro.OrdenarPor switch
        {
            "Titulo" => ordenarDesc ? query.OrderByDescending(t => t.Titulo) : query.OrderBy(t => t.Titulo),
            "Prioridade" => ordenarDesc ? query.OrderByDescending(t => t.Prioridade) : query.OrderBy(t => t.Prioridade),
            "Prazo" => ordenarDesc ? query.OrderByDescending(t => t.Prazo) : query.OrderBy(t => t.Prazo),
            _ => ordenarDesc ? query.OrderByDescending(t => t.CriadoEm) : query.OrderBy(t => t.CriadoEm),
        };

        var itens = await query
            .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
            .Take(filtro.TamanhoPagina)
            .ToListAsync();

        return (itens, total);
    }

    public async Task<TarefaResumo> ObterResumoAsync()
    {
        var agora = DateTime.UtcNow;
        return new TarefaResumo
        {
            Total = await _context.Tarefas.CountAsync(),
            Pendentes = await _context.Tarefas.CountAsync(t => t.Status == StatusTarefa.Pendente),
            EmAndamento = await _context.Tarefas.CountAsync(t => t.Status == StatusTarefa.EmAndamento),
            Concluidas = await _context.Tarefas.CountAsync(t => t.Status == StatusTarefa.Concluida),
            Atrasadas = await _context.Tarefas.CountAsync(t => t.Prazo != null && t.Prazo < agora && t.Status != StatusTarefa.Concluida)
        };
    }
}
