using Microsoft.EntityFrameworkCore;
using Tarefas.Models;
namespace Tarefas.Context;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Tarefa> Tarefas { get; set; }
}
