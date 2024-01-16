using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain.Interfaces.IRepositories;
using Todo.Api.Domain.Services;
using Todo.Api.Infrastructure.Data.DbContexts;
using Todo.Api.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

// Add DbContext
builder.Services.AddDbContext<TodoDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbConnection")));

// Add Dependency injection tokens
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<TasksService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<AuthenticationService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
