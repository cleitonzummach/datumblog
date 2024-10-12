using Datum.Blog.Domain.Entities;
using Datum.Blog.Domain.Interfaces;
using Datum.Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BlogDbContext _context;

        public UsuarioRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Inserir(Usuario entity)
        {
            _context.Usuario.Add(entity);
            _context.SaveChanges();
        }

        public async Task<Usuario?> ValidarLogin(string email, string senha)
        {
            return await _context.Usuario
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        public async Task<bool> VerificarExisteEmail(string email)
        {
            return await _context.Usuario.AnyAsync(u => u.Email == email);
        }

        public Task<Usuario> RetornarPorIdAsync(int id) { throw new NotImplementedException(); }

        public void Atualizar(Usuario entity) { throw new NotImplementedException(); }

        public void Excluir(Usuario entity) { throw new NotImplementedException(); }

        public Task<IEnumerable<Usuario>> RetornarTodosAsync() { throw new NotImplementedException(); }
    }
}
