using DailyPlanner.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DailyPlanner.Dal
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;

        public Repository(PlannerContext context)
        {
            _context = context;
        }

        public virtual ValueTask<TEntity> GetAsync(Guid id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public virtual Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            int skip, 
            int take)
        {
            return _context.Set<TEntity>()
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<TEntity>> GetAsync()
        {
            return _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var set = _context.Set<TEntity>();
            var found = await set.FindAsync(id);

            if (found == null)
            {
                return;
            }

            set.Remove(found);
        }

        public virtual TEntity Upsert(TEntity entity)
        {
           return _context.Set<TEntity>().Update(entity).Entity;
        }
    }
}
