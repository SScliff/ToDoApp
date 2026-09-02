using Microsoft.AspNetCore.Mvc;
using Todo.Application.Requests;
using Todo.Application.UseCases;
using Todo.Domain.Entities;

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
    [ProducesResponseType(typeof(TodoItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(TodoItem), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemRequest request)
    {
        var result = await createTodoItemUseCase.ExecuteAsync(request);
        
        if (result.IsFailed)
                return BadRequest(result.Errors);
        return CreatedAtAction(nameof(CreateTodoItem),result.Value);
    }

    [HttpGet]
    [EndpointSummary("Lista os ToDo com filtro, ordenação e paginação")]
    [EndpointDescription("Retorna as ToDo filtradas por status, prioridade, categoria e busca textual no título, combináveis entre si, ordenadas por Prazo/Prioridade/Criado Em e paginadas. Lista vazia retorna 200 com array vazio.")]
    [ProducesResponseType(typeof(ListTodoItemsUseCase.TodoItemListResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTodoItems([FromQuery] ListTodoItemsRequest request)
    {
        var result = await listTodoItemsUseCase.ExecuteAsync(request);
        if (result.IsFailed) 
            return BadRequest(result.Errors);
            
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Busca um ToDo pelo Id")]
    [EndpointDescription("Retorna os dados completos de um ToDo específico a partir do seu identificador.")]
    [ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoItemById(Guid id)
    {
        var result = await getTodoItemByIdUseCase.ExecuteAsync(id);
        if (result.IsFailed)
            return NotFound(result.Errors);
        return Ok(result.Value);
    }
}