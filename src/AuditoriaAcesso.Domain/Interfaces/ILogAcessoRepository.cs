using AuditoriaAcesso.Domain.Entities;

namespace AuditoriaAcesso.Domain.Interfaces;

public interface ILogAcessoRepository
{
    Task AdicionarAsync(LogAcesso logAcesso);
}
