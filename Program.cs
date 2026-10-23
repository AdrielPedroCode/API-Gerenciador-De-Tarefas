using GerenciadorTarefas.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Gerenciador de Tarefas API",
        Version = "v1",
        Description =  "API simples para cadastrar e acompanhar tarefas."

    });
});


builder.Services.AddDbContext<TarefasDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Tarefas")
        ?? "Data Source=gerenciador-tarefas.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TarefasDbContext>();
    db.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
