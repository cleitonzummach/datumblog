using Datum.Blog.Application.Dtos;
using Datum.Blog.Application.Interfaces;
using Datum.Blog.Domain.Entities;
using Datum.Blog.Domain.Interfaces;

namespace Datum.Blog.Application.Services
{
    public class PublicacaoService : IPublicacaoService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        public PublicacaoService(IPublicacaoRepository publicacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
        }

        /// <summary>
        /// Recebe o id do usuário e os dados da publicação informados para cadastro no banco de dados.
        /// </summary>
        /// <param name="usuarioId">Id do usuário que está cadastrando a publicação</param>
        /// <param name="publicacaoDto">Dados da publicação</param>
        /// <returns>Retorna um valor booleano do resultado da operação no banco de dados.</returns>
        public bool CriarPublicacao(int usuarioId, CriarPublicacaoDto publicacaoDto)
        {
            try
            {
                Publicacao publicacao = new Publicacao(publicacaoDto.Titulo, publicacaoDto.Conteudo, usuarioId);
                _publicacaoRepository.Inserir(publicacao);
                return publicacao.PublicacaoId > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao criar a publicação: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica se o usuário pode editar a publicação informada.
        /// </summary>
        /// <param name="usuarioId">Id do usuário que está realizando a operação de edição.</param>
        /// <param name="publicacaoId">Id da publicação que será editada.</param>
        /// <returns>Retorna um valor booleano com a validação da permissão de edição.</returns>
        public async Task<bool> VerificarEditarPublicacao(int usuarioId, int publicacaoId) 
        {
            Publicacao publicacao = await _publicacaoRepository.RetornarPorIdAsync(publicacaoId);
            return publicacao.UsuarioId == usuarioId;
        }

        /// <summary>
        /// Recebe o id do usuário e os dados da publicação para edição no banco de dados.
        /// </summary>
        /// <param name="usuarioId">Id do usuário que está realizando a edição da publicação.</param>
        /// <param name="publicacaoDto">Dados da publicação que serão editados.</param>
        /// <returns>Retorna um valor booleano do resultado da operação no banco de dados.</returns>
        public async Task<bool> EditarPublicacao(int usuarioId, EditarPublicacaoDto publicacaoDto)
        {
            try
            {
                Publicacao publicacao = await _publicacaoRepository.RetornarPorIdAsync(publicacaoDto.PublicacaoId);

                if (!string.IsNullOrEmpty(publicacaoDto.Titulo))
                    publicacao.Titulo = publicacaoDto.Titulo;

                if (!string.IsNullOrEmpty(publicacaoDto.Conteudo))
                    publicacao.Conteudo = publicacaoDto.Conteudo;
                
                _publicacaoRepository.Atualizar(publicacao);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao editar a publicação: {ex.Message}");
            }
        }

        /// <summary>
        /// Recebe o id da publicação que deverá realizar a operação de exclusão no banco de dados.
        /// </summary>
        /// <param name="publicacaoId">Id da publicação que será excluído.</param>
        public async Task ExcluirPublicacao(int publicacaoId)
        {
            try
            {
                Publicacao publicacao = await _publicacaoRepository.RetornarPorIdAsync(publicacaoId);
                _publicacaoRepository.Excluir(publicacao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao excluir a publicação: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica se o id da publicação está cadastrado no banco de dados.
        /// </summary>
        /// <param name="publicacaoId">Id da publicação que será verificado.</param>
        /// <returns>Retorna um booleano conforme a consulta do id da publicação no banco de dados.</returns>
        public async Task<bool> VerificarExistePublicacao(int publicacaoId)
        {
            return await _publicacaoRepository.ValidarExisteIdAsync(publicacaoId);
        }

        /// <summary>
        /// Retorna todos os registros da publicação cadastrados no banco de dados.
        /// </summary>
        /// <param name="titulo">Parâmetro opcional que filtra as publicações pelo título.</param>
        /// <returns>Retorna a lista de publicações cadastradas no banco de dados.</returns>
        public async Task<IEnumerable<Publicacao>> ConsultarPublicacao(string titulo)
        {
            if (!string.IsNullOrEmpty(titulo))
            {
                return await _publicacaoRepository.RetornarPorTituloAsync(titulo);
            }
            else
            {
                return await _publicacaoRepository.RetornarTodosAsync();
            }
        }
    }
}
