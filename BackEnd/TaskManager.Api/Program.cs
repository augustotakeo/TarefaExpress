using TaskManager.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder => 
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();