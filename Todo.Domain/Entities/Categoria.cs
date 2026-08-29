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

    public void AlterarNome(string nome)
    {
        if (nome.Length < 2 || nome.Length > 50)
            throw new ArgumentException("o nome deve ter entre 2 e 50 caracteres.");
        Nome = nome;
    }

    public void AlterarCor(string cor)
    {
        if (cor.Length != 7)
            throw new ArgumentException("a cor deve ter exatamente 7 caracteres (#RRGGBB).");
        Cor = cor;
    }
}