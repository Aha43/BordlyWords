using BordlyWords.Specification.Api;
using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Api
{
    public sealed class DefaultBordlyWordsApi : IBordlyWordsApi
    {
        private readonly Dictionary<CultureInfo, WordsOfCulture> _words = new();

        public Task<bool> IsWordAsync(ICheckWordParam p, CancellationToken cancellationToken = default) => Task.FromResult(GetWordsOfCulture(p.Culture).IsWord(p.Word));

        public Task<string?> GetWordAsync(IGetWordParam p, CancellationToken cancellationToken = default)
        {
            var words = GetWordsOfCulture(p.Culture);
            var retVal = words.GetWord(p);
            return Task.FromResult(retVal);
        }

        public Task LoadWordsAsync(ILoadWordsParam p, CancellationToken cancellationToken = default)
        {
            _words[p.Culture] = new WordsOfCulture(p);
            return Task.CompletedTask;
        }

        //// Helpers

        private WordsOfCulture GetWordsOfCulture(CultureInfo culture)
        {
            if (_words.TryGetValue(culture, out var words)) return words;
            throw new ArgumentException($"No words for cultur {culture.Name}");
        }

    }

}
