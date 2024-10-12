using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Datum.Blog.Domain.Entities;
using Datum.Blog.Domain.Interfaces;

namespace Datum.Blog.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Recebe os dados do usuário informado e realiza o cadastro no banco de dados.
        /// </summary>
        /// <param name="usuarioDto">Dados do usuário</param>
        /// <returns>Retorna um valor booleano conforme o resultado da operação no banco de dados.</returns>
        public bool CriarUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                Usuario usuario = new Usuario(usuarioDto.Nome, usuarioDto.Email, usuarioDto.Senha);
                _usuarioRepository.Inserir(usuario);
                return usuario.UsuarioId > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao criar o usuário: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica se já existe o e-mail cadastrado no banco de dados.
        /// </summary>
        /// <param name="email">Endereço de e-mail para ser verificado.</param>
        /// <returns>Retorna o valor booleano conforme o resultado da verificação no banco de dados.</returns>
        public async Task<bool> VerificarExisteEmail(string email)
        {
            return await _usuarioRepository.VerificarExisteEmail(email);
        }

        /// <summary>
        /// Valida se as credenciais do usuário estão cadastradas no banco de dados.
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Retorna o objeto Usuário com os dados encontrados no banco de dados.</returns>
        public async Task<Usuario> ValidarLogin(string email, string senha)
        {
            return await _usuarioRepository.ValidarLogin(email, senha);
        }
    }
}
