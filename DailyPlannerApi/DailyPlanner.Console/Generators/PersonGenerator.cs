using DailyPlanner.Console.Generators.SimpleGenerators;
using DailyPlanner.Entities;
using DailyPlanner.Infrastructure;
using System;
using System.Collections.Generic;

namespace DailyPlanner.Console.Generators
{
    class PersonGenerator
    {
        private PasswordHashManager _passwordHashManager;
        private string[] _namesSample;
        private readonly TextGenerator _textGenerator;

        private List<Person> _personData;

        public PersonGenerator(PasswordHashManager passwordHashManager, string[] namesSample, TextGenerator textGenerator)
        {
            _passwordHashManager = passwordHashManager;
            _namesSample = namesSample;
            _textGenerator = textGenerator;
        }

        /// <summary>
        /// Format: (fname)(space)(lastname)
        /// </summary>
        /// <param name="namesSample"></param>
        public List<Person> Generate()
        {
            _personData = new List<Person>();

            foreach (var namePair in _namesSample)
            {
                var fName = namePair.Split(" ")[0];
                var lName = namePair.Split(" ")[1];
                var password = fName.ToLowerInvariant();
                var passwordHashPair = _passwordHashManager.GenerateHashSaltPair(password);
                _personData.Add(
                    new Person()
                    {
                         FirstName = fName,
                         LastName = lName,
                         CreatedAt = DateTime.Now,
                         Email = $"{lName.ToLowerInvariant()}.{fName.ToLowerInvariant()}@mail.com",
                         Id = Guid.NewGuid(),
                         PasswordHash = passwordHashPair.hash,
                         Salt = passwordHashPair.salt
                    });
            }

            return _personData;
        }
    }
}
