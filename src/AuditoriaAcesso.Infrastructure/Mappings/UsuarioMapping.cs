using AuditoriaAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditoriaAcesso.Infrastructure.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SenhaHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(x => x.Logs)
            .WithOne(x => x.Usuario)
            .HasForeignKey("UsuarioId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
