namespace Todo.Domain.Specifications;
using Todo.Domain.Enums;
using Todo.Domain.Entities;

public class TarefaFiltro
{
    public StatusTarefa? Status { get; set; }
    public PrioridadeTarefa?  Prioridade { get; set; }
    public Guid? CategoriaId { get; set; }
    public string? Busca { get; set; }
    public string OrdenarPor { get; set; } = "CriadoEm";
    public string OrdenarPorDirecao { get; set; } = "asc";
    public int Pagina { get; set; } = 1;
    public int TamanhoPagina { get; set; } = 10;
}