using System.Threading.Tasks;

namespace DailyPlanner.Entities
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
