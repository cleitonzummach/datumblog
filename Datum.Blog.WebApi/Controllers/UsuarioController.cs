using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Datum.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Recebe os dados referente ao usuário e realiza o cadastro no banco de dados.
        /// </summary>
        /// <param name="usuarioDto">Dados do usuário.</param>
        /// <returns>HttpCode</returns>
        [HttpPost("Criar")]
        public async Task<IActionResult> Criar([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                // Verifica se já existe o e-mail informado vinculado com outro usuário.
                bool existeEmail = await _usuarioService.VerificarExisteEmail(usuarioDto.Email);

                if (!existeEmail)
                {
                    bool usuarioCriado = _usuarioService.CriarUsuario(usuarioDto);

                    if (usuarioCriado)
                        return Ok();
                    else
                        return BadRequest("Não foi possível criar o usuário.");
                }
                else
                {
                    return BadRequest("O e-mail informado já está sendo utilizado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
