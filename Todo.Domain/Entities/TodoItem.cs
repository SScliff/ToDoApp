using Todo.Domain.Enums;
using Todo.Domain.Exceptions;
namespace Todo.Domain.Entities;

public class TodoItem : BaseEntity
{
    /// <example>Estudar Entity Framework Core</example>
    public string Title { get; private set; }

    /// <example>Migrations, DbContext e Fluent API</example>
    public string? Description { get; private set; }

    /// <example>Pending</example>
    public TodoItemStatus Status { get; private set; }

    /// <example>Medium</example>
    public TodoItemPriority Priority { get; private set; }

    /// <example>2026-09-10T00:00:00</example>
    public DateTime? DueDate { get; private set; }

    /// <example>null</example>
    public DateTime? CompletedAt { get; private set; }

    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid? CategoryId { get; private set; }

    public Category? Category { get; private set; }


    public TodoItem(string title, string? description, TodoItemPriority priority, DateTime? dueDate)
    {
        if (description?.Length > 1000)
            throw new DomainException("A descrição deve ter no máximo 1000 caracteres.");

        if (string.IsNullOrWhiteSpace(title) || title.Length < 3 || title.Length > 120)
            throw new DomainException("O título deve ter entre 3 e 120 caracteres.");

        Title = title;
        Description = description;
        Status = TodoItemStatus.Pending;
        Priority = priority;
        DueDate = dueDate;
    }

    public void ChangeTitle(string title)
    {
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePriority(TodoItemPriority priority)
    {
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(TodoItemStatus newStatus)
    {
        if (Status == newStatus)
            return;
        if (newStatus == TodoItemStatus.Completed)
            CompletedAt = DateTime.UtcNow;
        else if (Status == TodoItemStatus.Completed)
            CompletedAt = null;

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

}
