using AuditoriaAcesso.Domain.Entities;
using AuditoriaAcesso.Domain.Enums;
using AuditoriaAcesso.Infrastructure.Context;

namespace AuditoriaAcesso.Infrastructure.Data;

public static class DbInitializer
{
    public static void SeedAdmin(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        const string emailAdmin = "admin@admin.com";

        if(!context.Usuarios.Any(u => u.Email == emailAdmin))
        {
            string senhaHash = BCrypt.Net.BCrypt.HashPassword("admin123");

            var admin = new Usuario("Administrador", emailAdmin, senhaHash, UsuarioRole.Admin);

            context.Usuarios.Add(admin);
            context.SaveChanges();
        }
    }
}
