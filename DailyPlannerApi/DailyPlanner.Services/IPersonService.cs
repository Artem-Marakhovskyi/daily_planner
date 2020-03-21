using DailyPlanner.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public interface IPeopleService
    {
        Task<PersonDto> GetAsync(string email, string password);

        Task<IEnumerable<PersonDto>> GetAsync();

        Task<PersonDto> GetAsync(string email);

        /// <exception cref="DailyPlanner.Services.Exceptions.NotUniqueException">If person not enique</exception>
        Task<PersonDto> CreateAsync(PersonDto personDto, string password);
    }
}
