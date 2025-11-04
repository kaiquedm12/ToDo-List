using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodolistApi.Data;
using TodolistApi.Models;

namespace TodoListApi.Controllers;

/// <summary>
/// Controlador REST para operações CRUD de tarefas (TodoItems).
/// As rotas seguem o padrão `api/todo` (pelo atributo [Route]).
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    // Contexto do EF injetado pelo construtor - usado para acessar a base de dados
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor onde o <see cref="AppDbContext"/> é injetado pelo container.
    /// </summary>
    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todas as tarefas cadastradas.
    /// GET: api/todo
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }

    /// <summary>
    /// Retorna uma tarefa pelo id.
    /// GET: api/todo/{id}
    /// Retorna 404 se não encontrada.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// POST: api/todo
    /// Retorna 201 Created com local da nova entidade.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    /// <summary>
    /// Atualiza uma tarefa existente pelo id.
    /// PUT: api/todo/{id}
    /// Retorna 400 se o id da rota não bater com o id do payload.
    /// Retorna 204 NoContent em caso de sucesso.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem item)
    {
        if (id != item.Id) return BadRequest();

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove uma tarefa pelo id.
    /// DELETE: api/todo/{id}
    /// Retorna 404 se não existir; 204 se removida com sucesso.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
