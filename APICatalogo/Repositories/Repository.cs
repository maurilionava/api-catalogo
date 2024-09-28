using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            // Método SET permite acessar uma coleção ou tabela
            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

            return entity;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Método SET permite acessar uma coleção ou tabela
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public T Update(T entity)
        {
            // Gera comando SQL que atualizará toda a tabela
            _context.Set<T>().Update(entity);

            // Gera comando SQL que atualizará somente a entidade atualizada
            //_context.Entry(entity).State = EntityState.Modified;

            return entity;
        }
    }
}
