using Todo.Domain.Enums;

namespace Todo.Application.Requests;

/// <summary>
/// Filtros, ordenação e paginação da listagem de tarefas. Todos os campos são opcionais
/// e combináveis entre si — por exemplo, <c>status=Pending&amp;priority=High</c>.
/// </summary>
public record ListTodoItemsRequest
{
    /// <summary>
    /// Filtra por status: <c>Pending</c>, <c>InProgress</c>, <c>Completed</c> ou <c>Cancelled</c>.
    /// Omitido, retorna tarefas de todos os status.
    /// </summary>
    /// <example>Pending</example>
    public TodoItemStatus? Status { get; init; }

    /// <summary>
    /// Filtra por prioridade: <c>Low</c>, <c>Medium</c> ou <c>High</c>.
    /// Omitido, retorna tarefas de todas as prioridades.
    /// </summary>
    /// <example>High</example>
    public TodoItemPriority? Priority { get; init; }

    /// <summary>
    /// Retorna apenas as tarefas vinculadas a esta categoria.
    /// Omitido, retorna tarefas de todas as categorias e também as sem categoria.
    /// </summary>
    /// <example>9c1f4b7e-2d3a-4f56-8b90-1e2d3c4b5a6f</example>
    public Guid? CategoryId { get; init; }

    /// <summary>Busca textual por trecho no título ou na descrição da tarefa.</summary>
    /// <example>categorias</example>
    public string? Search { get; init; }

    /// <summary>
    /// Campo usado na ordenação: <c>CreatedAt</c> (padrão), <c>Title</c>, <c>Priority</c> ou <c>DueDate</c>.
    /// Valores não reconhecidos usam <c>CreatedAt</c>.
    /// </summary>
    /// <example>DueDate</example>
    public string SortBy { get; init; } = "CreatedAt";

    /// <summary>Sentido da ordenação: <c>asc</c> (padrão) ou <c>desc</c>.</summary>
    /// <example>desc</example>
    public string SortDirection { get; init; } = "asc";

    /// <summary>Página desejada, começando em 1. Valores menores que 1 são tratados como 1.</summary>
    /// <example>1</example>
    public int Page { get; init; } = 1;

    /// <summary>
    /// Quantidade de itens por página. Padrão 10, máximo 100 —
    /// valores acima do teto são reduzidos a 100.
    /// </summary>
    /// <example>20</example>
    public int PageSize { get; init; } = 10;
}
