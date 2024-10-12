using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Datum.Blog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Datum.Blog.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public TokenService(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Recebe as credenciais do usuário e, caso seja um usuário válido, retorna a chave do token de autenticação.
        /// </summary>
        /// <param name="tokenDto">Dados do usuário para autenticação.</param>
        /// <returns>Retorna o token de autenticação.</returns>
        public async Task<string> RetornarToken(TokenDto tokenDto)
        {
            // Valida se as credenciais informadas do usuário são válidas.
            Usuario usuario = await _usuarioService.ValidarLogin(tokenDto.Email, tokenDto.Senha);

            if (usuario != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Busca a chave secreta do JWT do arquivo de configuração.
                var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));

                var tokenPropriedades = new SecurityTokenDescriptor()
                {
                    // Dados do usuário dentro do token.
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }),

                    // Tempo de expiração do token.
                    Expires = DateTime.UtcNow.AddHours(1),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografia), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenPropriedades);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
