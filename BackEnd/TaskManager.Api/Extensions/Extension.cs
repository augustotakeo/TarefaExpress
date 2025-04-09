using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Api.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<TaskContext>();
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
    }
}