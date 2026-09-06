using Todo.Domain.Entities;

namespace Todo.Application.Responses;

/// <summary>
/// Representação de uma categoria retornada pela API.
/// </summary>
public record CategoryResponse
{
    /// <summary>Identificador da categoria, gerado pelo servidor na criação.</summary>
    /// <example>9c1f4b7e-2d3a-4f56-8b90-1e2d3c4b5a6f</example>
    public Guid Id { get; init; }

    /// <summary>Nome da categoria, de 2 a 50 caracteres. Único entre todas as categorias.</summary>
    /// <example>Backend</example>
    public required string Name { get; init; }

    /// <summary>Cor de exibição em hexadecimal, no formato <c>#RRGGBB</c>.</summary>
    /// <example>#5B34D6</example>
    public required string Color { get; init; }

    /// <summary>Data de criação, em UTC. Definida pelo servidor e nunca alterada.</summary>
    /// <example>2026-09-28T10:12:44Z</example>
    public DateTime CreatedAt { get; init; }

    /// <summary>Data da última alteração, em UTC.</summary>
    /// <example>2026-09-28T10:12:44Z</example>
    public DateTime UpdatedAt { get; init; }

    /// <summary>Monta a resposta a partir da entidade de domínio.</summary>
    public static CategoryResponse FromEntity(Category item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Color = item.Color,
        CreatedAt = item.CreatedAt,
        UpdatedAt = item.UpdatedAt,
    };
}
