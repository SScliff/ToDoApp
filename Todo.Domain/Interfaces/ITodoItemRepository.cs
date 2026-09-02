using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Specifications;
namespace Todo.Domain.Interfaces;

public interface ITodoItemRepository
{
    Task AddAsync(TodoItem todoItem);
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(Guid id);
    Task<(List<TodoItem> Items, int Total)> ListAsync(
        TodoItemStatus? status = null,
        TodoItemPriority? priority = null,
        Guid? categoryId = null,
        string? search = null,
        string sortBy = "CreatedAt",
        string sortDirection = "asc",
        int page = 1,
        int pageSize = 10);
    Task<TodoItemSummary> GetSummaryAsync();
}
