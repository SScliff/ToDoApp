namespace Todo.Domain.Entities;

public class BaseEntity
{
    /// <summary>Identificador do registro, gerado pelo servidor na criação.</summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>Data de criação, em UTC. Definida pelo servidor e nunca alterada.</summary>
    /// <example>2026-10-01T13:45:02Z</example>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>Data da última alteração, em UTC. Igual a <c>createdAt</c> enquanto o registro não é editado.</summary>
    /// <example>2026-10-14T09:32:10Z</example>
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
}