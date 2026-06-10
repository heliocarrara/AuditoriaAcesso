namespace AuditoriaAcesso.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }

    private readonly List<LogAcesso> _logs = new List<LogAcesso>();
    public IReadOnlyCollection<LogAcesso> Logs => _logs.AsReadOnly();

    protected Usuario()
    {

    }

    public Usuario(string nome, string email, string senhaHash)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
    }

    public bool PodeSerExcluido()
    {
        //Aqui verifica se o usuário tem logs de acesso, se tiver, não pode ser excluído
        return _logs.Count == 0;
    }
}
