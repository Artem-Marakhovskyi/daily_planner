using System;
using System.Linq;

namespace DailyPlanner.Console.Generators.SimpleGenerators
{
    public class TextGenerator
    {
        private readonly Random _random;
        private readonly string[] _sample;

        public TextGenerator(string sample)
        {
            _random = new Random();
            _sample = sample.Split(". ");
        }

        public string GetSentence()
        {
            return _sample[_random.Next(_sample.Length)] + ".";
        }

        public string GetWord()
        {
            var sentence = GetSentence();
            var splittedSentence = sentence
                .Split(',', '.', ' ', ':', ';', '!', '@')
                .Where(e => e.Length > 4)
                .ToList();

            if (!splittedSentence.Any())
            {
                splittedSentence.Add($"self-generated {_random.Next(int.MaxValue)}");
            }


            var word = splittedSentence[_random.Next(splittedSentence.Count)].ToLowerInvariant();

            return word.Substring(0, 1).ToUpperInvariant() + word.Substring(1);
        }

    }
}
