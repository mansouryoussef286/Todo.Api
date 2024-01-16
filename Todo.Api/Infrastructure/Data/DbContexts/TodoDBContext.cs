using System;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Infrastructure.Data.Models;

namespace Todo.Api.Infrastructure.Data.DbContexts
{
    public partial class TodoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
    }
}
//dotnet ef migrations add [MigrationName]

//dotnet ef database update