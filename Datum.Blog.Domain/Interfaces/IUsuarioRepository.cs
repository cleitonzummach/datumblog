using Datum.Blog.Domain.Entities;

namespace Datum.Blog.Domain.Interfaces
{
    /// <summary>
    /// Métodos pertinentes a entidade do Usuário.
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ValidarLogin(string email, string senha);
        Task<bool> VerificarExisteEmail(string email);
    }
}
