using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Datum.Blog.Domain.Entities;
using Datum.Blog.WebApi.Hubs.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Datum.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoController : ControllerBase
    {
        private readonly IPublicacaoService _publicacaoService;
        private readonly INotificacaoHub _notificacaoHub;

        public PublicacaoController(IPublicacaoService publicacaoService, INotificacaoHub notificacaoHub)
        {
            _publicacaoService = publicacaoService;
            _notificacaoHub = notificacaoHub;
        }

        /// <summary>
        /// Recebe os dados da publicação e realiza o cadastro no banco de dados.
        /// </summary>
        /// <param name="publicacaoDto">Dados da publicação.</param>
        /// <returns>HttpCode</returns>
        [HttpPost("Criar")]
        [Authorize]
        public async Task<IActionResult> Criar([FromBody] CriarPublicacaoDto publicacaoDto)
        {
            try
            {
                // Recupera o id do usuário armazenado dentro do token de autenticação.
                int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                bool publicacaoCriada = _publicacaoService.CriarPublicacao(usuarioId, publicacaoDto);

                if (publicacaoCriada)
                {
                    // Envia a notificação para todos os clientes conectados.
                    await _notificacaoHub.EnviarMensagem("Uma nova publicação foi publicada e está disponível para leitura.");
                    return Ok();
                }
                else
                {
                    return BadRequest("Não foi possível criar a publicação.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recebe os dados da publicação que deverão sofrer alterações no banco de dados.
        /// </summary>
        /// <param name="publicacaoDto">Dados da publicação que serão alterados.</param>
        /// <returns>HttpCode</returns>
        [HttpPut("Editar")]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] EditarPublicacaoDto publicacaoDto)
        {
            try
            {
                // Recupera o id do usuário armazenado dentro do token de autenticação.
                int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Verifica se existe o id da publicação informado.
                bool existePublicacao = await _publicacaoService.VerificarExistePublicacao(publicacaoDto.PublicacaoId);

                if (existePublicacao)
                {
                    // Verifica se o id do usuário que está editando a publicação é o mesmo id do usuário que fez o cadastro da publicação.
                    bool podeEditar = await _publicacaoService.VerificarEditarPublicacao(usuarioId, publicacaoDto.PublicacaoId);

                    if (podeEditar)
                    {
                        bool publicacaoEditada = await _publicacaoService.EditarPublicacao(usuarioId, publicacaoDto);

                        if (publicacaoEditada)
                            return Ok();
                        else
                            return BadRequest("Não foi possível criar a publicação.");
                    }
                    else
                    {
                        return BadRequest("A publicação informada não é de sua autoria.");
                    }
                }
                else
                {
                    return BadRequest("Não foi possível localizar o id da publicação informado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recebe o id da publicação que deverá ser excluída do banco de dados.
        /// </summary>
        /// <param name="publicacaoId">Id da publicação para exclusão.</param>
        /// <returns>HttpCode</returns>
        [HttpDelete("Excluir")]
        [Authorize]
        public async Task<IActionResult> Excluir(int publicacaoId)
        {
            try
            {
                // Verifica se existe o id da publicação informado.
                bool existePublicacao = await _publicacaoService.VerificarExistePublicacao(publicacaoId);

                if (existePublicacao)
                {
                    await _publicacaoService.ExcluirPublicacao(publicacaoId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Não foi possível localizar o id da publicação informado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Consulta todas as publicações cadastradas no banco de dados.
        /// </summary>
        /// <param name="titulo">Parâmetro opcional para filtrar as publicação pelo título.</param>
        /// <returns>Retorna as publicações encontradas no banco de dados.</returns>
        [HttpGet("Consultar")]
        [AllowAnonymous]
        public async Task<IActionResult> Consultar(string? titulo)
        {
            try
            {
                List<ConsultarPublicacaoRetornoDto> listRetorno = new List<ConsultarPublicacaoRetornoDto>();
                
                IEnumerable<Publicacao> listPublicacao = await _publicacaoService.ConsultarPublicacao(titulo);

                if (listPublicacao.Any())
                {
                    foreach (var publicacao in listPublicacao)
                    {
                        listRetorno.Add(new ConsultarPublicacaoRetornoDto(publicacao.PublicacaoId, publicacao.Titulo, publicacao.Conteudo, publicacao.Data, publicacao.Usuario.Nome, publicacao.Usuario.Email));
                    }
                }

                return Ok(listRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
