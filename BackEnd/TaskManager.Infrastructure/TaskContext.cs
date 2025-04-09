using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.EntityConfigurations;

namespace TaskManager.Infrastructure;

public class TaskContext : DbContext
{
    public DbSet<TaskItem> TaskItens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=127.0.0.1,1433;Database=TaskManager;Trusted_Connection=False;ConnectRetryCount=0;TrustServerCertificate=true;User Id=SA;Password=12345Aa!");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder build)
    {
        build.ApplyConfiguration(new TaskItemConfiguration());
    }
}