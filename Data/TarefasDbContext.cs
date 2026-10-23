using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Data;

public class TarefasDbContext(DbContextOptions<TarefasDbContext> options) : DbContext(options)
{
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
}
