using Datum.Blog.Domain.Entities;

namespace Datum.Blog.Domain.Interfaces
{
    /// <summary>
    /// Métodos pertinentes a entidade da Publicação.
    /// </summary>
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        Task<bool> ValidarExisteIdAsync(int id);
        Task<IEnumerable<Publicacao>> RetornarPorTituloAsync(string titulo);
    }
}
