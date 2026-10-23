using GerenciadorTarefas.Data;
using GerenciadorTarefas.DTOs;
using GerenciadorTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorTarefas.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TarefasController(TarefasDbContext db) : ControllerBase {
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TarefaRespostaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TarefaRespostaDto>>> Listar() {
        var tarefas = await db.Tarefas.AsNoTracking().OrderBy(t => t.Id).ToListAsync();
        return Ok(tarefas.Select(ParaResposta));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TarefaRespostaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TarefaRespostaDto>> BuscarPorId(int id) {
        var tarefa = await db.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return tarefa is null ? NotFound() : Ok(ParaResposta(tarefa));
    }


    [HttpPost]
    [ProducesResponseType(typeof(TarefaRespostaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TarefaRespostaDto>> Criar(CriarTarefaDto dto) {
        var tarefa = new Tarefa {
            Titulo = dto.Titulo.Trim(),
            Descricao = dto.Descricao?.Trim(),
            Concluida = false
        };

        db.Tarefas.Add(tarefa);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = tarefa.Id }, ParaResposta(tarefa));
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, AtualizarTarefaDto dto) {
        var tarefa = await db.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();

        tarefa.Titulo = dto.Titulo.Trim();
        tarefa.Descricao = dto.Descricao?.Trim();
        tarefa.Concluida = dto.Concluida;


        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id) {

        var tarefa = await db.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();

        db.Tarefas.Remove(tarefa);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static TarefaRespostaDto ParaResposta(Tarefa tarefa) =>
        new(tarefa.Id, tarefa.Titulo, tarefa.Descricao, tarefa.Concluida, tarefa.CriadaEm);
}
