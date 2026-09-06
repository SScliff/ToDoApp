using Todo.Domain.Enums;
using Todo.Domain.Exceptions;
namespace Todo.Domain.Entities;

public class TodoItem : BaseEntity
{
    /// <summary>Título da tarefa, de 3 a 120 caracteres.</summary>
    /// <example>Revisar o PR do CRUD de categorias</example>
    public string Title { get; private set; }

    /// <summary>Detalhamento livre da tarefa, até 1000 caracteres. Nulo quando não informado.</summary>
    /// <example>Conferir o 409 de nome duplicado e a exclusão de categoria com tarefas vinculadas.</example>
    public string? Description { get; private set; }

    /// <summary>
    /// Situação atual: <c>Pending</c>, <c>InProgress</c>, <c>Completed</c> ou <c>Cancelled</c>.
    /// Toda tarefa nasce em <c>Pending</c>.
    /// </summary>
    /// <example>InProgress</example>
    public TodoItemStatus Status { get; private set; }

    /// <summary>Prioridade da tarefa: <c>Low</c>, <c>Medium</c> ou <c>High</c>.</summary>
    /// <example>High</example>
    public TodoItemPriority Priority { get; private set; }

    /// <summary>Prazo da tarefa, em UTC. Nulo quando a tarefa não tem prazo definido.</summary>
    /// <example>2026-10-15T18:00:00Z</example>
    public DateTime? DueDate { get; private set; }

    /// <summary>
    /// Momento da conclusão, em UTC. Preenchido quando o status passa a <c>Completed</c>
    /// e zerado se a tarefa for reaberta.
    /// </summary>
    /// <example>2026-10-14T09:32:10Z</example>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    /// Identificador da categoria à qual a tarefa pertence. Nulo quando a tarefa não tem categoria.
    /// Corresponde ao <c>id</c> do objeto <c>category</c>.
    /// </summary>
    public Guid? CategoryId { get; private set; }

    /// <summary>Dados da categoria vinculada. Retornado apenas na busca por id.</summary>
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
