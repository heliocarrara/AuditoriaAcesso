using AuditoriaAcesso.Domain.Entities;

namespace AuditoriaAcesso.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> ObterPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task AdicionarAsync(Usuario usuario);
    void Deleter(Usuario usuario);
    Task SalvarAlteracoesAsync();
}
