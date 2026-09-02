using Todo.Domain.Enums;

namespace Todo.Application.Requests;

public record ListTodoItemsRequest
{
    /// <example>Pending</example>
    public TodoItemStatus? Status { get; init; }

    /// <example>High</example>
    public TodoItemPriority? Priority { get; init; }

    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid? CategoryId { get; init; }

    /// <example>Estudar</example>
    public string? Search { get; init; }

    /// <example>CreatedAt</example>
    public string SortBy { get; init; } = "CreatedAt";

    /// <example>desc</example>
    public string SortDirection { get; init; } = "asc";

    /// <example>1</example>
    public int Page { get; init; } = 1;

    /// <example>10</example>
    public int PageSize { get; init; } = 10;
}