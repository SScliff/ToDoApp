using Todo.Domain.Exceptions;
namespace Todo.Domain.Entities;

public class Category : BaseEntity
{
    /// <summary>Nome da categoria, de 2 a 50 caracteres. Único entre todas as categorias.</summary>
    /// <example>Backend</example>
    public string Name { get; private set; }

    /// <summary>Cor de exibição em hexadecimal, no formato <c>#RRGGBB</c>.</summary>
    /// <example>#5B34D6</example>
    public string Color { get; private set; }


    public Category(string name, string color)
    {
        if (string.IsNullOrWhiteSpace(color) || color.Length != 7)
            throw new DomainException("a cor deve ter exatamente 7 caracteres (#RRGGBB).");
        if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 50)
            throw new DomainException("o nome deve ter entre 2 e 50 caracteres.");

        Name = name;
        Color = color;
    }

    public void ChangeName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeColor(string color)
    {
        Color = color;
        UpdatedAt = DateTime.UtcNow;
    }
    
}
