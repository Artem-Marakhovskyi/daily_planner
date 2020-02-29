using DailyPlanner.Dto.Notes;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetAsync(string email, string password);
    }
}
