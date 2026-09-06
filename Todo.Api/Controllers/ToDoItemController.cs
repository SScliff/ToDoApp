using Microsoft.AspNetCore.Mvc;
using Todo.Application.Requests;
using Todo.Application.Responses;
using Todo.Application.UseCases;

namespace Todo.Api.Controllers;

[Route("api/todo")]
[ApiController]
public class ToDoItemController(
    CreateTodoItemUseCase createTodoItemUseCase, 
    ListTodoItemsUseCase listTodoItemsUseCase,
    GetTodoItemByIdUseCase getTodoItemByIdUseCase) 
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Cria um novo ToDo")]
    [EndpointDescription("Recebe Título, Descrição, Prioridade e Prazo e cria um ToDo com Status Pendente")]
    [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemRequest request)
    {
        var result = await createTodoItemUseCase.ExecuteAsync(request);

        if (result.IsFailed)
            return Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro de validação");

        return CreatedAtAction(
            nameof(GetTodoItemById),
            new { id = result.Value.Id },
            result.Value);
    }

    [HttpGet]
    [EndpointSummary("Lista os ToDo com filtro, ordenação e paginação")]
    [EndpointDescription("Retorna as ToDo filtradas por status, prioridade, categoria e busca textual no título, combináveis entre si, ordenadas por Prazo/Prioridade/Criado Em e paginadas. Lista vazia retorna 200 com array vazio.")]
    [ProducesResponseType(typeof(ListTodoItemsUseCase.TodoItemListResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListTodoItems([FromQuery] ListTodoItemsRequest request)
    {
        var result = await listTodoItemsUseCase.ExecuteAsync(request);

        if (result.IsFailed)
            return Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro de validação");

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [EndpointSummary("Busca um ToDo pelo Id")]
    [EndpointDescription("Retorna os dados completos de um ToDo específico a partir do seu identificador.")]
    [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoItemById(Guid id)
    {
        var result = await getTodoItemByIdUseCase.ExecuteAsync(id);

        if (result.IsFailed)
            return Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso não encontrado");

        return Ok(result.Value);
    }
}