using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.DTOs;

public class CriarTarefaDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 120 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição pode ter no máximo 500 caracteres.")]
    public string? Descricao { get; set; }
}
