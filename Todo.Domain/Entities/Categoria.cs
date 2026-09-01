using Todo.Domain.Exceptions;
namespace Todo.Domain.Entities;

public class Categoria
{
    public Guid IdDaCategoria {get; private set;}
    public string Nome {get; private set;}
    public string Cor {get; private set;}


    public Categoria(string nome, string cor)
    {
        ValidarNome(nome);
        ValidarCor(cor);

        IdDaCategoria = Guid.NewGuid();
        Nome = nome;
        Cor = cor;
    }

    public void AlterarNome(string nome)
    {
        ValidarNome(nome);
        Nome = nome;
    }

    public void AlterarCor(string cor)
    {
        ValidarCor(cor);
        Cor = cor;
    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Length < 2 || nome.Length > 50)
            throw new DomainException("o nome deve ter entre 2 e 50 caracteres.");
    }

    private static void ValidarCor(string cor)
    {
        if (string.IsNullOrWhiteSpace(cor) || cor.Length != 7)
            throw new DomainException("a cor deve ter exatamente 7 caracteres (#RRGGBB).");
    }
}