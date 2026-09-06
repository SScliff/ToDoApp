using Todo.Api.ExceptionHandlers;

namespace Todo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<DomainExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi(options =>
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
        return services;
    }
    
}