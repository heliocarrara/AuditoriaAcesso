using AuditoriaAcesso.Aplication.Dtos;
using AuditoriaAcesso.Domain.Entities;
using AuditoriaAcesso.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuditoriaAcesso.Aplication.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepositorie;
    private readonly ILogAcessoRepository _logAcessoRepositorie;
    private readonly IConfiguration _configuration;
    public UsuarioAppService(IUsuarioRepository usuarioRepository, ILogAcessoRepository logAcessoRepository, IConfiguration configuration)
    {
        _usuarioRepositorie = usuarioRepository;
        _logAcessoRepositorie = logAcessoRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> AutenticarAsync(LoginDto loginDto)
    {
        var users = await _usuarioRepositorie.ObterTodosAsync();

        var user = users.FirstOrDefault(x => x.Email.Equals(loginDto.Email, StringComparison.OrdinalIgnoreCase));

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, user.SenhaHash))
        {
            throw new UnauthorizedAccessException("Email ou senha inválidos!");
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var token  = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginResponseDto(user.Email, tokenHandler.WriteToken(token));
    }

    public async Task<UsuarioResponseDto> CadastrarAsync(UsuarioCadastroDto usuarioCadastroDto, string ipAddress)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioCadastroDto.Senha);

        var novoUsuario = new Usuario(usuarioCadastroDto.Nome, usuarioCadastroDto.Email, senhaHash, Domain.Enums.UsuarioRole.User);

        novoUsuario.RegistrarAcesso(ipAddress);

        await _usuarioRepositorie.AdicionarAsync(novoUsuario);

        await _usuarioRepositorie.SalvarAlteracoesAsync();

        return new UsuarioResponseDto(novoUsuario.Id, novoUsuario.Nome, novoUsuario.Email, 1);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var usuario = await _usuarioRepositorie.ObterPorIdAsync(id);

        if (usuario == null)
        {
            return false;
        }

        if (!usuario.PodeSerExcluido())
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

        foreach (var usuario in usuarios)
        {
            var qntLogs = usuario.Logs?.Count ?? 0;
            usuariosResponse.Add(new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email, qntLogs));
        }

        return usuariosResponse;
    }
}
