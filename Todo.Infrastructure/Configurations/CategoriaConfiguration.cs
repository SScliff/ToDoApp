namespace Todo.Infrastructure.Configurations;
using Todo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(c => c.Nome)
            .IsUnique();
        builder.Property(c => c.Cor)
            .IsRequired()
            .HasMaxLength(7);
    }
}