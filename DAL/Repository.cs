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

    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task UpdateAsync(T entity) => _dbSet.Update(entity);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) _dbSet.Remove(entity);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}

}
