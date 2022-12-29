using BordlyWords.Specification.Api;
using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Api
{
    public sealed class DefaultBordlyWordsApi : IBordlyWordsApi
    {
        private readonly Dictionary<string, WordDictionary> _words = new();

        public Task<bool> IsWordAsync(ICheckWordParam p, CancellationToken cancellationToken = default) => Task.FromResult(GetWordsOfCulture(p.Key()).IsWord(p.Word));

        public Task<string?> GetWordAsync(IGetWordParam p, CancellationToken cancellationToken = default)
        {
            var words = GetWordsOfCulture(p.Key());
            var retVal = words.GetWord(p);
            return Task.FromResult(retVal);
        }

        public Task LoadWordsAsync(ILoadWordsParam p, CancellationToken cancellationToken = default)
        {
            p.EnsureLoadWordsParamParam();

            var dict = new WordDictionary(p);
            _words[dict.Key] = new WordDictionary(p);
            return Task.CompletedTask;
        }

        // Helpers

        private WordDictionary GetWordsOfCulture(string key)
        {
            if (_words.TryGetValue(key, out var words)) return words;
            throw new ArgumentException($"No words for cultur {key}");
        }

    }

}
