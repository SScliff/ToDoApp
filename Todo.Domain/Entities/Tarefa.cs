using Todo.Domain.Enums;
using Todo.Domain.Exceptions;
namespace Todo.Domain.Entities;

public class Tarefa
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string? Descricao { get; private set; }
    public StatusTarefa Status { get; private set; }
    public PrioridadeTarefa Prioridade { get; private set; }
    public DateTime? Prazo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? AtualizadoEm { get; private set; }
    public DateTime? ConcluidoEm { get; private set; }
    public Guid? CategoriaId { get; private set; }
    public Categoria? Categoria { get; private set; }


    public Tarefa(string titulo, string? descricao, PrioridadeTarefa prioridade, DateTime? prazo)
    {
        ValidarTitulo(titulo);
        ValidarDescricao(descricao);

        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Status = StatusTarefa.Pendente;
        Prioridade = prioridade;
        Prazo = prazo;
        CriadoEm = DateTime.UtcNow;
    }

    public void AlterarTitulo(string titulo)
    {
        ValidarTitulo(titulo);
        Titulo = titulo;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AlterarDescricao(string? descricao)
    {
        ValidarDescricao(descricao);
        Descricao = descricao;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AlterarPrioridade(PrioridadeTarefa prioridade)
    {
        Prioridade = prioridade;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AlterarPrazo(DateTime? prazo)
    {
        Prazo = prazo;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AssociarCategoria(Guid? categoriaId)
    {
        CategoriaId = categoriaId;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AlterarStatus(StatusTarefa novoStatus)
    {
        if (Status == novoStatus)
            return;
        if (novoStatus == StatusTarefa.Concluida)
            ConcluidoEm = DateTime.UtcNow;
        else if (Status == StatusTarefa.Concluida)
            ConcluidoEm = null;

        Status = novoStatus;
        AtualizadoEm = DateTime.UtcNow;
    }

    private static void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 3 || titulo.Length > 120)
            throw new DomainException("O título deve ter entre 3 e 120 caracteres.");
    }

    private static void ValidarDescricao(string? descricao)
    {
        if (descricao?.Length > 1000)
            throw new DomainException("A descrição deve ter no máximo 1000 caracteres.");
    }
}

