namespace GerenciadorTarefas.DTOs;

public record TarefaRespostaDto(int Id, string Titulo, string? Descricao, bool Concluida, DateTime CriadaEm);
