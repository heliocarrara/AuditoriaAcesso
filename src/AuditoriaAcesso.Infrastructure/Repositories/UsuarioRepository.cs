using AuditoriaAcesso.Domain.Entities;
using AuditoriaAcesso.Domain.Interfaces;
using AuditoriaAcesso.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAcesso.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UsuarioRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AdicionarAsync(Usuario usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);
    }

    public void Deletar(Usuario usuario)
    {
        _dbContext.Usuarios.Remove(usuario);
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _dbContext.Usuarios
            .Include(x => x.Logs)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _dbContext.Usuarios
            .Include(x => x.Logs)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
