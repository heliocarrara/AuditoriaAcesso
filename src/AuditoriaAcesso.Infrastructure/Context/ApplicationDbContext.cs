using AuditoriaAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAcesso.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<LogAcesso> LogAcesso { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
