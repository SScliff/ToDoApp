using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Specifications;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoDbContext _context;

    public TodoItemRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _context.TodoItems
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
        if (todoItem == null)
            return;

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task<(List<TodoItem> Items, int Total)> ListAsync(
        TodoItemStatus? status = null,
        TodoItemPriority? priority = null,
        Guid? categoryId = null,
        string? search = null,
        string sortBy = "CreatedAt",
        string sortDirection = "asc",
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.TodoItems.AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority);

        if (categoryId.HasValue)
            query = query.Where(t => t.CategoryId == categoryId);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(t => t.Title.Contains(search) || (t.Description != null && t.Description.Contains(search)));

        var total = await query.CountAsync();

        var sortDescending = sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);
        query = sortBy switch
        {
            "Title" => sortDescending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
            "Priority" => sortDescending ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority),
            "DueDate" => sortDescending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
            _ => sortDescending ? query.OrderByDescending(t => t.CreatedAt) : query.OrderBy(t => t.CreatedAt),
        };

        var safePage = Math.Max(page, 1);
        var safePageSize = Math.Clamp(pageSize, 1, 100);

        var items = await query
            .Skip((safePage - 1) * safePageSize)
            .Take(safePageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task<TodoItemSummary> GetSummaryAsync()
    {
        var now = DateTime.UtcNow;
        return new TodoItemSummary
        {
            Total = await _context.TodoItems.CountAsync(),
            Pending = await _context.TodoItems.CountAsync(t => t.Status == TodoItemStatus.Pending),
            InProgress = await _context.TodoItems.CountAsync(t => t.Status == TodoItemStatus.InProgress),
            Completed = await _context.TodoItems.CountAsync(t => t.Status == TodoItemStatus.Completed),
            Overdue = await _context.TodoItems.CountAsync(t => t.DueDate != null && t.DueDate < now && t.Status != TodoItemStatus.Completed)
        };
    }
}
