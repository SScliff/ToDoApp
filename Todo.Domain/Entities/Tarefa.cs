using Todo.Domain.Enums;
namespace Todo.Domain.Entities;

public class Tarefa
{
    public Guid Id { get; private set; }
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public StatusTarefa Status { get; private set; }
    public PrioridadeTarefa Prioridade { get; set; }
    public DateTime? Prazo { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? ConcluidoEm { get; set; }
    public Guid? CategoriaId { get; set; }                                                                                                                                 
    public Categoria? Categoria { get; set; }


    public Tarefa(string titulo, string? descricao, PrioridadeTarefa prioridade, DateTime? prazo)
    {
        if (titulo.Length < 3 || titulo.Length > 120)
            throw new ArgumentException("O título deve ter entre 3 e 120 caracteres.");
        
        if (descricao?.Length > 1000)
            throw new ArgumentException("a descricao deve ter no max 1000 caracteres.");
                
                
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Status = StatusTarefa.Pendente;
        Prioridade = prioridade;
        Prazo = prazo;
        CriadoEm = DateTime.UtcNow;
    }

    public void AlterarStatus(StatusTarefa NovoStatus)
    {
        if (Status == NovoStatus)
            return;
        if (NovoStatus == StatusTarefa.Concluida)
            ConcluidoEm = DateTime.UtcNow;
        else if (Status == StatusTarefa.Concluida)
            ConcluidoEm = null;
        
        Status = NovoStatus;
        AtualizadoEm = DateTime.UtcNow;
    }
        
}

