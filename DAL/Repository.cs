using Microsoft.EntityFrameworkCore;
using PixNote.Models;
using System.Linq.Expressions;

namespace PixNote.DAL
{
    public class Repository<T> : IRep<T> where T : class
    {
        private readonly PhotoDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(PhotoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

}
