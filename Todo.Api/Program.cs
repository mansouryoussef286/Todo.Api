using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain.Interfaces.IRepositories;
using Todo.Api.Domain.Services;
using Todo.Api.Infrastructure.Data.DbContexts;
using Todo.Api.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add appsettings
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:4200", "")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddHttpClient();

// Add DbContext
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbConnection")));

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
// its not used none the less
app.UseAuthentication();

app.MapControllers();
app.UseCors();
app.Run();
