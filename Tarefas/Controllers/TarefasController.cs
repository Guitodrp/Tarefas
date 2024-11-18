using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Models;

namespace Tarefas.Controllers;

[Route("api/tarefas")]
[ApiController]
public class TarefasController(Context.Context context) : ControllerBase
{
    //Retorna uma lista de tarefas
    [HttpGet("obter-todas")]
    public async Task<ActionResult<List<Tarefa>>> GetAllTarefas() => await context.Tarefas.ToListAsync();

    //Retorna uma lista de pelo titulo
    [HttpGet("obter-por-titulo")]
    public async Task<ActionResult<List<Tarefa>>> GetTarefasTitulo(string titulo)
    {
        return await context.Tarefas.Where(s => s.Titulo.ToLower() == titulo.ToLower()).ToListAsync();
    }

    //Retorna uma lista de tarefas pela data
    [HttpGet("obter-por-data")]
    public async Task<ActionResult<List<Tarefa>>> GetTarefasData(DateTime data)
    {
        return await context.Tarefas.Where(s => s.Data == data).ToListAsync();
    }

    //Retorna uma lista de tarefas pelo status
    [HttpGet("obter-por-status")]
    public async Task<ActionResult<List<Tarefa>>> GetTarefasStatus(Models.Tarefa.EStatus status)
    {
        return await context.Tarefas.Where(s => s.Status == status).ToListAsync();
    }

    //Retorna uma tarefa de acordo com o ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> GetTarefa(Guid id)
    {
        var tarefa = await context.Tarefas.FindAsync(id);

        if (tarefa == null)
            return NotFound();

        return tarefa;
    }

    //Edit uma tarefa de acordo com o ID
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarefa(Guid id, Tarefa tarefa)
    {
        var tarefaBanco = await context.Tarefas.FindAsync(id);

        if (tarefaBanco == null)
            return NotFound();

        tarefaBanco.Titulo = tarefa.Titulo;
        tarefaBanco.Descricao = tarefa.Descricao;
        tarefaBanco.Data = DateTime.Now;
        tarefa.Status = Tarefa.EStatus.Pendente;

        await context.SaveChangesAsync();

        return Ok();
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
    {
        context.Tarefas.Add(tarefa);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetTarefa", new { id = tarefa.Id }, tarefa);
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarefa(Guid id)
    {
        var tarefa = await context.Tarefas.FindAsync(id);
        if (tarefa == null)
            return NotFound();

        context.Tarefas.Remove(tarefa);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
