namespace TodolistApi.Models;

/// <summary>
/// Representa uma tarefa (to-do) simples.
/// Esta classe mapeia a entidade armazenada no banco de dados.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Identificador único da tarefa (chave primária).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título ou descrição curta da tarefa.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Indica se a tarefa foi marcada como concluída.
    /// </summary>
    public bool IsCompleted { get; set; }
}