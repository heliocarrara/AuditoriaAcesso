namespace AuditoriaAcesso.Aplication.Dtos;

public record UsuarioCadastroDto(string Nome, string Email, string Senha);

public record UsuarioResponseDto(int Id, string Nome, string Email, int QtdLogsAcesso);

public record LoginDto(string Email, string Senha);
public record LoginResponseDto(string Email, string Token);
