using Microsoft.EntityFrameworkCore;
using TodolistApi.Models;

namespace TodolistApi.Data;

/// <summary>
/// DbContext principal da aplicação. Configura o acesso ao banco de dados
/// e expõe os DbSets que representam as tabelas/entidades.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Construtor onde as opções (connection string, provider, etc) são injetadas.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   
    /// <summary>
    /// Conjunto de tarefas (mapeado para a tabela de TodoItems).
    /// Tornado anulável (`DbSet<TodoItem>?`) para evitar uso de inicializadores null-forgiving.
    /// O EF Core popula esta propriedade em tempo de execução quando o contexto é usado.
    /// </summary>
    public DbSet<TodoItem>? TodoItems { get; set; }
   
}