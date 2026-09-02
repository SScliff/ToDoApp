using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
namespace Todo.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(c => c.Name)
            .IsUnique();
        builder.Property(c => c.Color)
            .IsRequired()
            .HasMaxLength(7);
    }
}
