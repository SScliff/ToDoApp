using Todo.Domain.Entities;
using Todo.Domain.Enums;
namespace Todo.Domain.Interfaces;

public interface ITarefaRepository
{
    Task<Tarefa> CriarTarefaAsync(string titulo, string descricao, PrioridadeTarefa prioridade);
    Task<Tarefa> BuscarTarefaPorIdAsync(Guid id);
    Task EditarTarefaAsync(Tarefa tarefa);
    Task ExcluirTarefaAsync(Guid id);
}