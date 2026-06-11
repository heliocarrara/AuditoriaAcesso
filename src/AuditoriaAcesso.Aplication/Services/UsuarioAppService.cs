using AuditoriaAcesso.Aplication.Dtos;
using AuditoriaAcesso.Domain.Entities;
using AuditoriaAcesso.Domain.Interfaces;

namespace AuditoriaAcesso.Aplication.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepositorie;
    private readonly ILogAcessoRepository _logAcessoRepositorie;
    public UsuarioAppService(IUsuarioRepository usuarioRepository, ILogAcessoRepository logAcessoRepository)
    {
        _usuarioRepositorie = usuarioRepository;
        _logAcessoRepositorie = logAcessoRepository;
    }

    public async Task<UsuarioResponseDto> CadastrarAsync(UsuarioCadastroDto usuarioCadastroDto, string ipAddress)
    {
        var senhaHash  = BCrypt.Net.BCrypt.HashPassword(usuarioCadastroDto.Senha);

        var novoUsuario = new Usuario(usuarioCadastroDto.Nome, usuarioCadastroDto.Email, senhaHash);

        await _usuarioRepositorie.AdicionarAsync(novoUsuario);

        var log = new LogAcesso(ipAddress, novoUsuario.Id);
        await _logAcessoRepositorie.AdicionarAsync(log);

        await _usuarioRepositorie.SalvarAlteracoesAsync();

        return new UsuarioResponseDto(novoUsuario.Id, novoUsuario.Nome, novoUsuario.Email, 1);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var usuario = await _usuarioRepositorie.ObterPorIdAsync(id);

        if(usuario == null)
        {
            return false;
        }

        if(!usuario.PodeSerExcluido())
        {
            throw new InvalidOperationException("Este usuário não pode ser excluído!");
        }

        _usuarioRepositorie.Deletar(usuario);

        await _usuarioRepositorie.SalvarAlteracoesAsync();

        return true;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync()
    {
        var usuarios = await _usuarioRepositorie.ObterTodosAsync();

        var usuariosResponse = new List<UsuarioResponseDto>();

        foreach(var usuario in usuarios)
        {
            var qntLogs = usuario.Logs?.Count ?? 0;
            usuariosResponse.Add(new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email, qntLogs));
        }

        return usuariosResponse;
    }
}
