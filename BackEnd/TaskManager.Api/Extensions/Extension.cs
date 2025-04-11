using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Filters;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Api.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskDatabase")));
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddControllersWithViews(options => options.Filters.Add<ExceptionFilter>());
    }
}