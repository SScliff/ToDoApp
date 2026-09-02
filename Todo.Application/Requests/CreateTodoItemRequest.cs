using Todo.Domain.Enums;

namespace Todo.Application.Requests;

public record CreateTodoItemRequest
{
    /// <example>Estudar Entity Framework Core</example>
    public string Title { get; init; }

    /// <example>Migrations, DbContext e Fluent API</example>
    public string Description { get; init; }

    /// <example>Medium</example>
    public TodoItemPriority Priority { get; init; }

    /// <example>2026-09-10T00:00:00</example>
    public DateTime DueDate { get; init; }
}
