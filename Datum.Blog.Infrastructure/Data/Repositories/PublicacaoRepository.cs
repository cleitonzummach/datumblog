using Datum.Blog.Domain.Entities;
using Datum.Blog.Domain.Interfaces;
using Datum.Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.Infrastructure.Data.Repositories
{
    public class PublicacaoRepository : IPublicacaoRepository
    {
        private readonly BlogDbContext _context;

        public PublicacaoRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publicacao>> RetornarTodosAsync()
        {
            return await _context.Publicacao
                                 .Include(p => p.Usuario)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Publicacao>> RetornarPorTituloAsync(string titulo)
        {
            return await _context.Publicacao
                                 .Where(p => p.Titulo.Contains(titulo))
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Publicacao> RetornarPorIdAsync(int id)
        {
            return await _context.Publicacao
                                 .Include(p => p.Usuario)
                                 .FirstAsync(x => x.PublicacaoId == id);
        }

        public async Task<bool> ValidarExisteIdAsync(int id)
        {
            return await _context.Publicacao
                                 .AsNoTracking()
                                 .AnyAsync(x => x.PublicacaoId == id);
        }

        public void Inserir(Publicacao entity)
        {
            _context.Publicacao.Add(entity);
            _context.SaveChanges();
        }

        public void Atualizar(Publicacao entity)
        {
            _context.SaveChanges();
        }

        public void Excluir(Publicacao entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
