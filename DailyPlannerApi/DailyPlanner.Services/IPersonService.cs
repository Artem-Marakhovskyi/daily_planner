using DailyPlanner.Dto.Notes;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetAsync(string email, string password);

        /// <exception cref="DailyPlanner.Services.Exceptions.NotUniqueException">If person not enique</exception>
        Task<PersonDto> CreateAsync(PersonDto personDto, string password);
    }
}
