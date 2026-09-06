using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Application.Responses;

/// <summary>
/// Representação de uma tarefa retornada pela API.
/// </summary>
public record TodoItemResponse
{
    /// <summary>Identificador da tarefa, gerado pelo servidor na criação.</summary>
    /// <example>7d3e1f92-4a6b-4c58-9e01-2b8f5a7c3d64</example>
    public Guid Id { get; init; }

    /// <summary>Título da tarefa, de 3 a 120 caracteres.</summary>
    /// <example>Revisar o PR do CRUD de categorias</example>
    public required string Title { get; init; }

    /// <summary>Detalhamento livre da tarefa. Nulo quando não informado.</summary>
    /// <example>Conferir o 409 de nome duplicado e a exclusão de categoria com tarefas vinculadas.</example>
    public string? Description { get; init; }

    /// <summary>
    /// Situação atual: <c>Pending</c>, <c>InProgress</c>, <c>Completed</c> ou <c>Cancelled</c>.
    /// </summary>
    /// <example>InProgress</example>
    public TodoItemStatus Status { get; init; }

    /// <summary>Prioridade da tarefa: <c>Low</c>, <c>Medium</c> ou <c>High</c>.</summary>
    /// <example>High</example>
    public TodoItemPriority Priority { get; init; }

    /// <summary>Prazo da tarefa, em UTC. Nulo quando a tarefa não tem prazo definido.</summary>
    /// <example>2026-10-15T18:00:00Z</example>
    public DateTime? DueDate { get; init; }

    /// <summary>
    /// Momento da conclusão, em UTC. Preenchido quando o status passa a <c>Completed</c>
    /// e zerado se a tarefa for reaberta.
    /// </summary>
    /// <example>2026-10-14T09:32:10Z</example>
    public DateTime? CompletedAt { get; init; }

    /// <summary>
    /// Identificador da categoria da tarefa. Nulo quando a tarefa não tem categoria.
    /// Sempre presente, mesmo quando <c>category</c> não vem preenchido.
    /// </summary>
    /// <example>9c1f4b7e-2d3a-4f56-8b90-1e2d3c4b5a6f</example>
    public Guid? CategoryId { get; init; }

    /// <summary>
    /// Dados completos da categoria. Retornado na busca por id; nulo na listagem.
    /// </summary>
    public CategoryResponse? Category { get; init; }

    /// <summary>Data de criação, em UTC. Definida pelo servidor e nunca alterada.</summary>
    /// <example>2026-10-01T13:45:02Z</example>
    public DateTime CreatedAt { get; init; }

    /// <summary>Data da última alteração, em UTC.</summary>
    /// <example>2026-10-14T09:32:10Z</example>
    public DateTime UpdatedAt { get; init; }

    /// <summary>Monta a resposta a partir da entidade de domínio.</summary>
    public static TodoItemResponse FromEntity(TodoItem item)
    {
        CategoryResponse? category = null;
        if (item.Category is not null)
            category = CategoryResponse.FromEntity(item.Category);

        return new TodoItemResponse
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status,
            Priority = item.Priority,
            DueDate = item.DueDate,
            CompletedAt = item.CompletedAt,
            CategoryId = item.CategoryId,
            Category = category,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
        };
    }
}
