using System.Threading.Tasks;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Notes;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(PlannerContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
            => _context.SaveChangesAsync();
    }
}
