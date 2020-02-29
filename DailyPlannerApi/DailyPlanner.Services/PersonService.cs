using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Entities;
using DailyPlanner.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        public PersonService(
            IPasswordHashManager passwordHashManager,
            IRepository<Person> personRepository,
            IMapper mapper)
        {
            _passwordHashManager = passwordHashManager;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonDto> GetAsync(string email, string password)
        {
            var people = await _personRepository
                .GetAsync(e => e.Email == email, 0, 1);

            var person = people.FirstOrDefault();

            if (person == null
                || !_passwordHashManager.IsPasswordValid(
                    password,
                    person.PasswordHash,
                    person.Salt))
            {
                return null;
            }

            return _mapper.Map<PersonDto>(person);
        }
    }
}
