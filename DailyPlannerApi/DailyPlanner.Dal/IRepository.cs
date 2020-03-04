using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DailyPlanner.Entities;

namespace DailyPlanner.Dal
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        ValueTask<TEntity> GetAsync(Guid id);
        Task<List<TEntity>> GetAsync();
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            int skip, 
            int take);
        Task RemoveAsync(Guid id);
        TEntity Upsert(TEntity entity);
    }
}
