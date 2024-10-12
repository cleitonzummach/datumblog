using Datum.Blog.Application.Dtos;
using Datum.Blog.Domain.Entities;

namespace Datum.Blog.Application.Interfaces
{
    public interface IUsuarioService
    {
        bool CriarUsuario(UsuarioDto usuarioDto);
        Task<bool> VerificarExisteEmail(string email);
        Task<Usuario> ValidarLogin(string email, string senha);
    }
}
