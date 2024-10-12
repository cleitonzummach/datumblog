using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Datum.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Recebe como parâmetro as credenciais do usuário e retorna o token de autenticação.
        /// </summary>
        /// <param name="tokenDto">Credenciais do usuário.</param>
        /// <returns>Retorna o token de autenticação.</returns>
        [HttpPost("Gerar")]
        public async Task<IActionResult> Gerar([FromBody] TokenDto tokenDto)
        {
            var token = await _tokenService.RetornarToken(tokenDto);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
