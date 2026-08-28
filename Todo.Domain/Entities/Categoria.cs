namespace Todo.Domain.Entities;

public class Categoria
{
    public Guid IdDaCategoria {get; private set;}
    public string Nome {get; private set;}
    public string Color {get; private set;}
    
    
    Categoria(string nome, string color)
    {
        IdDaCategoria = Guid.NewGuid();    
        Nome = nome;
        Color = color;
    }
}