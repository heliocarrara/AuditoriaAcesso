using AuditoriaAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditoriaAcesso.Infrastructure.Mappings;

public class LogAcessoMapping : IEntityTypeConfiguration<LogAcesso>
{
    public void Configure(EntityTypeBuilder<LogAcesso> builder)
    {
        builder.ToTable("LogAcesso");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataAcesso)
            .IsRequired();

        builder.Property(x => x.IpAddress)
            .IsRequired()
            .HasMaxLength(45); // IPv6 tem esse tamanho máximo

    }
}
