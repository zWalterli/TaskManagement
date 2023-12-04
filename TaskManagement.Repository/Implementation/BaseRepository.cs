using System.Linq.Expressions;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Repository.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseDbModel
    {
        protected readonly APIContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(APIContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? default!;
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, Object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (includes is not null && includes.Any())
                foreach (var include in includes)
                    query = query.Include(include);

            var models = query.Where(predicate);
            return await Task.FromResult(models.ToList());
        }

        public virtual async Task<T> Insert(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<List<T>> Insert(List<T> itens)
        {
            await _dbSet.AddRangeAsync(itens);
            await _context.SaveChangesAsync();
            return itens;
        }

        public virtual async Task<T> Update(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<T>> Update(List<T> itens)
        {
            _dbSet.UpdateRange(itens);
            await _context.SaveChangesAsync();
            return itens;
        }

        public virtual async Task<bool> Delete(T item)
        {
            var itemDb = await GetById(item.Id.Value);
            _dbSet.Remove(itemDb);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}