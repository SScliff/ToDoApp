using Todo.Domain.Enums;

namespace Todo.Application.Requests;

/// <summary>
/// Dados para criação de uma tarefa. A tarefa nasce sempre com status <c>Pending</c>,
/// e o servidor define <c>id</c> e <c>createdAt</c> — esses campos são ignorados se enviados.
/// </summary>
public record CreateTodoItemRequest
{
    /// <summary>Título da tarefa. Obrigatório, de 3 a 120 caracteres.</summary>
    /// <example>Revisar o PR do CRUD de categorias</example>
    public required string Title { get; init; }

    /// <summary>Detalhamento livre da tarefa. Opcional, até 1000 caracteres.</summary>
    /// <example>Conferir o 409 de nome duplicado e a exclusão de categoria com tarefas vinculadas.</example>
    public string? Description { get; init; }

    /// <summary>Prioridade da tarefa: <c>Low</c>, <c>Medium</c> ou <c>High</c>.</summary>
    /// <example>High</example>
    public TodoItemPriority Priority { get; init; }

    /// <summary>
    /// Prazo da tarefa, em UTC. Opcional; quando informado, não pode ser uma data no passado.
    /// </summary>
    /// <example>2026-10-15T18:00:00Z</example>
    public DateTime? DueDate { get; init; }
}
