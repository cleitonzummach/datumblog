using Datum.Blog.Application.Dtos;
using Datum.Blog.Domain.Entities;

namespace Datum.Blog.Application.Interfaces
{
    public interface IPublicacaoService
    {
        bool CriarPublicacao(int usuarioId, CriarPublicacaoDto publicacaoDto);
        Task<bool> VerificarEditarPublicacao(int usuarioId, int publicacaoId);
        Task<bool> EditarPublicacao(int usuarioId, EditarPublicacaoDto publicacaoDto);
        Task ExcluirPublicacao(int publicacaoId);
        Task<bool> VerificarExistePublicacao(int publicacaoId);
        Task<IEnumerable<Publicacao>> ConsultarPublicacao(string titulo);
    }
}
