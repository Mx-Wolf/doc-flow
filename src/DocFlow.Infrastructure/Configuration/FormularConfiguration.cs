using DocFlow.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocFlow.Infrastructure.Configuration;
public class FormularConfiguration:IEntityTypeConfiguration<Formular>
{
    public void Configure(EntityTypeBuilder<Formular> builder)
    {
        builder.ToTable("Formulars");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(f => f.DocumentData)
            .HasConversion(
                a => a.AssemblyQualifiedName,
                s => Type.GetType(s!)!)
            .HasMaxLength(800);

        builder.OwnsOne(f => f.Presentable);
    }
}
