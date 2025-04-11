using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.EntityConfigurations;

namespace TaskManager.Infrastructure;

public class TaskContext : DbContext
{
    public DbSet<TaskItem> TaskItens { get; set; }

    public TaskContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder build)
    {
        build.ApplyConfiguration(new TaskItemConfiguration());
    }
}