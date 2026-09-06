using Microsoft.Extensions.DependencyInjection;
using Todo.Application.UseCases;

namespace Todo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTodoItemUseCase>();
        services.AddScoped<ListTodoItemsUseCase>();
        services.AddScoped <GetTodoItemByIdUseCase>();
        return services;
    }
}