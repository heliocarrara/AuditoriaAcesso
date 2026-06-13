using AuditoriaAcesso.Aplication.Dtos;

namespace AuditoriaAcesso.Aplication.Services;

public interface IUsuarioAppService
{
    Task<UsuarioResponseDto> CadastrarAsync(UsuarioCadastroDto usuarioCadastroDto, string ipAddress);

    Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync();
    Task<bool> ExcluirAsync(int id);
}
