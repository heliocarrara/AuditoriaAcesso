using AuditoriaAcesso.Aplication.Dtos;
using AuditoriaAcesso.Aplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditoriaAcesso.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioAppService _usuarioAppService;

    public UsuariosController(IUsuarioAppService usuarioAppService)
    {
        _usuarioAppService = usuarioAppService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDto dto)
    {
        if(dto == null)
        {
            return BadRequest("Dados de cadastro são obrigatórios.");
        }

        string ipAdress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconhecido";

        var resultado = await _usuarioAppService.CadastrarAsync(dto, ipAdress);

        return CreatedAtAction(nameof(ObterTodos), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioAppService.ObterTodosAsync();
        return Ok(usuarios);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            var excluido = await _usuarioAppService.ExcluirAsync(id);
            if (!excluido)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            return NoContent();
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
