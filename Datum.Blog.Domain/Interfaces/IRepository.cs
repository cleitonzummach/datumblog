namespace Datum.Blog.Domain.Interfaces
{
    /// <summary>
    /// Métodos com ações padrão de relacionamento das entidades com o banco de dados.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> RetornarPorIdAsync(int id);
        Task<IEnumerable<TEntity>> RetornarTodosAsync();
        void Inserir(TEntity entity);
        void Atualizar(TEntity entity);
        void Excluir(TEntity entity);
    }
}
