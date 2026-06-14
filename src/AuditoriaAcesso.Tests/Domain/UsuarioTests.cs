using AuditoriaAcesso.Domain.Entities;

namespace AuditoriaAcesso.Tests.Domain;

public class UsuarioTests
{
    [Fact]
    public void Deve_Permitir_Exclusao_Usuario_Sem_Logs()
    {
        var usuario = new Usuario("João Silva", "joao@email.com", "senha_hash_123", AuditoriaAcesso.Domain.Enums.UsuarioRole.User);

        var podeSerExcluido = usuario.PodeSerExcluido();

        Assert.True(podeSerExcluido);
    }

    [Fact]
    public void Não_Deve_Permitir_Exclusao_Usuario_Com_Logs()
    {
        var usuario = new Usuario("João Silva", "joao@email.com", "senha_hash_123", AuditoriaAcesso.Domain.Enums.UsuarioRole.User);
        usuario.RegistrarAcesso("192.168.1.1.");

        var podeSerExcluido = usuario.PodeSerExcluido();

        Assert.False(podeSerExcluido);
    }
}
