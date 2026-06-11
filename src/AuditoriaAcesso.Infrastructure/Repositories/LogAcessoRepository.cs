using AuditoriaAcesso.Domain.Entities;
using AuditoriaAcesso.Domain.Interfaces;
using AuditoriaAcesso.Infrastructure.Context;

namespace AuditoriaAcesso.Infrastructure.Repositories;

public class LogAcessoRepository : ILogAcessoRepository
{
    private readonly ApplicationDbContext _dbContext;
    public LogAcessoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AdicionarAsync(LogAcesso logAcesso)
    {
        _dbContext.LogAcesso.Add(logAcesso);
    }
}
