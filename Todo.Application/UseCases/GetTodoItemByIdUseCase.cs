using FluentResults;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.UseCases;

public class GetTodoItemByIdUseCase(ITodoItemRepository todoItemRepository)
{
    public async Task<Result<TodoItem>> ExecuteAsync(Guid id)
    {
        var todo = await todoItemRepository.GetByIdAsync(id);
        if (todo is null)
            return Result.Fail($"Não existe uma tarefa com o id {id}");
        return Result.Ok(todo);
    }
}