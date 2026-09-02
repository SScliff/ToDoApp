using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Todo.Application.UseCases;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Todo App API";
        document.Info.Version = "v1";
        document.Info.Description = "API REST para gerenciamento de tarefas (Tasks) e categorias (Categories) — desafio técnico .NET + React. " +
            "Camadas separadas em Domain, Application, Infrastructure e Api, com filtros/ordenação/paginação em tarefas e resumo de status.";
        return Task.CompletedTask;
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CreateTodoItemUseCase>();
builder.Services.AddScoped<ListTodoItemsUseCase>();
builder.Services.AddScoped <GetTodoItemByIdUseCase>();
var connectionString = builder.Configuration.GetConnectionString("todoDB");

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
