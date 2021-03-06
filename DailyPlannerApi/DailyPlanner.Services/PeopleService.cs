﻿using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto;
using DailyPlanner.Entities;
using DailyPlanner.Infrastructure;
using DailyPlanner.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public PeopleService(
            IPasswordHashManager passwordHashManager,
            IRepository<Person> personRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _passwordHashManager = passwordHashManager;
            _personRepository = personRepository;
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<PersonDto> CreateAsync(PersonDto personDto, string password)
        {
            if ((await _personRepository.GetAsync(p => p.Email == personDto.Email, 0, 1)).Any())
            {
                throw new NotUniqueException();
            }

            var person = _mapper.Map<Person>(personDto);

            var hashSaltPair = _passwordHashManager.GenerateHashSaltPair(password);
            person.PasswordHash = hashSaltPair.hash;
            person.Salt = hashSaltPair.salt;


            var newlyCreatedPerson = _personRepository.Upsert(person);
            
            await _uof.CommitAsync();

            return _mapper.Map<PersonDto>(newlyCreatedPerson);
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

        public async Task<IEnumerable<PersonDto>> GetAsync()
        {
            var personEntities = await _personRepository.GetAsync();

            return _mapper.Map<List<PersonDto>>(personEntities);
        }

        public async Task<PersonDto> GetAsync(string email)
        {
            var people = await _personRepository
                .GetAsync(e => e.Email == email, 0, 1);

            var person = people.FirstOrDefault();
            if (person == null)
            {
                return null;
            }

            return _mapper.Map<PersonDto>(person);
        }
    }
}
