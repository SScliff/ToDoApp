using Todo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Todo.Infrastructure.Configurations;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.Property(t => t.Titulo)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(t => t.Descricao)
            .HasMaxLength(1000);

        builder.HasOne<Categoria>(t => t.Categoria)
            .WithMany()
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }   
}