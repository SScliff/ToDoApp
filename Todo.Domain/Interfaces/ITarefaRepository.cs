using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Specifications;
namespace Todo.Domain.Interfaces;

public interface ITarefaRepository
{
    Task<Tarefa> CriarTarefaAsync(string titulo, string? descricao, PrioridadeTarefa prioridade, DateTime? prazo);
    Task<Tarefa?> BuscarTarefaPorIdAsync(Guid id);
    Task EditarTarefaAsync(Tarefa tarefa);
    Task ExcluirTarefaAsync(Guid id);
    Task<(List<Tarefa> Itens, int Total)> ListarTarefasAsync(TarefaFiltro filtro);
    Task<TarefaResumo> ObterResumoAsync();
}