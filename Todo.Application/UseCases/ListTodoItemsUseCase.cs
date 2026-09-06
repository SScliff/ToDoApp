using FluentResults;
using Todo.Application.Requests;
using Todo.Application.Responses;
using Todo.Domain.Interfaces;

namespace Todo.Application.UseCases;

public class ListTodoItemsUseCase(ITodoItemRepository todoItemRepository)
{
    public record TodoItemListResult(
        List<TodoItemResponse> Items,
        int Page,
        int PageSize,
        int TotalItems,
        int TotalPages);

    public async Task<Result<TodoItemListResult>> ExecuteAsync(ListTodoItemsRequest request)
    {
        var (items, total) = await todoItemRepository.ListAsync(
            request.Status,
            request.Priority,
            request.CategoryId,
            request.Search,
            request.SortBy,
            request.SortDirection,
            request.Page,
            request.PageSize
        );

        var responses = items.Select(TodoItemResponse.FromEntity).ToList();
        var totalPages = (int)Math.Ceiling(total / (double)request.PageSize);
        var result = new TodoItemListResult(responses, request.Page, request.PageSize, total, totalPages);

        return Result.Ok(result);
    }
}
