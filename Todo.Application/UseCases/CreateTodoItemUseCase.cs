using FluentResults;
using Todo.Application.Requests;
using Todo.Application.Responses;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.UseCases;

public class CreateTodoItemUseCase(ITodoItemRepository  TodoItemRepository)
{
    public async Task<Result<TodoItemResponse>> ExecuteAsync(CreateTodoItemRequest request)
    { 
        var todo = new TodoItem(request.Title, request.Description, request.Priority, request.DueDate);
        await TodoItemRepository.AddAsync(todo);
        var response = TodoItemResponse.FromEntity(todo);
        return Result.Ok(response);
    }
    

}