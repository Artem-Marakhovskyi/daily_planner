using System.Threading.Tasks;
using DailyPlanner.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
            => _context.SaveChangesAsync();
    }
}
