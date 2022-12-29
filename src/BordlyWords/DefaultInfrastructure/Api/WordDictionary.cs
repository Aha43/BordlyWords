using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Api
{
    internal sealed class WordDictionary
    {
        public CultureInfo Culture { get; private set; } = new CultureInfo("nb-NO");

        public string Name { get; private set; }

        public string Key => $"{Name}_{Culture.Name}";

        public string? Description { get; private set; }

        private readonly Dictionary<int, string[]> _words = new();

        private readonly int[] _lengths;

        private readonly string[]? _checkWords;

        public WordDictionary(ILoadWordsParam p) 
        {
            Name = p.Name; 
            Description = p.Description;

            var added = new HashSet<string>();

            var lenghts = new HashSet<int>();

            var tmp = new Dictionary<int, List<string>>();
            foreach (var word in p.Words)
            {
                var normalWord = word.ToLower();
                if (!added.Contains(normalWord))
                {
                    var length = word.Length;

                    if (length >= p.MinWordLength && length <= p.MaxWordLength) 
                    {
                        if (!tmp.ContainsKey(length)) tmp[length] = new();
                        tmp[length].Add(normalWord);

                        added.Add(normalWord);
                        lenghts.Add(length);
                    }
                }
            }

            foreach (var pair in tmp)
            {
                pair.Value.Sort();
                _words[pair.Key] = pair.Value.ToArray();
            }

            _lengths = lenghts.ToArray();

            _checkWords = p.CheckWords?.Select(w => w.ToLower()).ToArray();
            if (_checkWords != null) Array.Sort(_checkWords);
        }

        public bool IsWord(string word) 
        {
            var normalWord = word.ToLower();

            if (_checkWords != null)
            {
                return Array.BinarySearch(_checkWords, normalWord) > -1;
            }
            if (_words.TryGetValue(normalWord.Length, out var words)) return Array.BinarySearch(words, normalWord) > -1;
            return false;
        }

        private readonly Random _random = new();

        public string? GetWord(IGetWordParam p)
        {
            var length = p.Length; 
            if (length == null)
            {
                if (_lengths.Length== 0) return null;
                var idx = _random.Next(_lengths.Length);
                var words = _words[idx];
                idx = _random.Next(words.Length);
                return words[idx];
            }

            if (_lengths.Any(e => e == length))
            {
                var words = _words[length.Value];
                var idx = _random.Next(words.Length);
                return words[idx];
            }

            return null;
        }

    }

}
