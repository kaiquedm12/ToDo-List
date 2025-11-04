namespace TodolistApi.Models;
using System.ComponentModel.DataAnnotations;

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
    /// Agora declarado como anulável para evitar inicializadores null-forgiving.
    /// </summary>
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string? Title { get; set; }

    /// <summary>
    /// Indica se a tarefa foi marcada como concluída.
    /// </summary>
    public bool IsCompleted { get; set; } = false;
}