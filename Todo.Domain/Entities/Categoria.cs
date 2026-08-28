namespace Todo.Domain.Entities;

public class Categoria
{
    public Guid IdDaCategoria {get; private set;}
    public string Nome {get; private set;}
    public string Cor {get; private set;}
    
    
    public Categoria(string nome, string cor)
    {
        IdDaCategoria = Guid.NewGuid();    
        Nome = nome;
        Cor = cor;
    }
}