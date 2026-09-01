namespace TodoApi
using Todo.Infrastructure.Data;

builder.Services.AddOpenApi();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("todo_db"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("todo_db"))
    )
);

var app = builder.Builder();