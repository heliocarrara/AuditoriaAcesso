namespace AuditoriaAcesso.Domain.Entities;

public class LogAcesso
{
    public int Id { get; private set; }
    public DateTime DataAcesso { get; private set; }
    public string IpAddress { get; private set; }
    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }

    protected LogAcesso()
    {
    }

    public LogAcesso(string ipAddress, int usuarioId)
    {
        this.DataAcesso = DateTime.UtcNow;
        this.IpAddress = ProcessarIpAddress(ipAddress);
        this.UsuarioId = usuarioId;
    }

    private string ProcessarIpAddress(string ipAddress)
    {
        return string.IsNullOrWhiteSpace(ipAddress) ? ipAddress : "0.0.0.0";
    }
}
