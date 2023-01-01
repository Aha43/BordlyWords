using BordlyWords.DefaultInfrastructure.Domain;
using BordlyWords.Specification.Api;
using BordlyWords.Specification.Domain;
using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.DefaultInfrastructure.Api
{
    public sealed class DefaultBordlyWordsApi : IBordlyWordsApi
    {
        private readonly Dictionary<string, WordDictionary> _words = new();

        public Task<IWordCheck> Check(ICheckWordParam p, CancellationToken cancellationToken = default)
        {
            var isWord = GetWordsOfCulture(p.Key()).Check(p.Word);
            var retVal = new WordCheck { IsWord = isWord, Word = p.Word };
            return Task.FromResult(retVal as IWordCheck);
        }

        public Task<IPickedWord> Pick(IPickWordParam p, CancellationToken cancellationToken = default)
        {
            var words = GetWordsOfCulture(p.Key());
            var word = words.Pick(p);
            var retVal = new PickedWord { Word = word };
            return Task.FromResult(retVal as IPickedWord);
        }

        public Task LoadWordsAsync(ILoadWordsParam p, CancellationToken cancellationToken = default)
        {
            p.EnsureLoadWordsParamParam();

            var dict = new WordDictionary(p);
            _words[dict.Key] = dict;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IWordDictionaryInfo>> GetInfo(CancellationToken cancellationToken = default)
        {
            var list = new List<IWordDictionaryInfo>();
            foreach (var word in _words.Values) list.Add(word.GetDictionaryInfo());
            return Task.FromResult(list.AsEnumerable());
        }

        // Helpers

        private WordDictionary GetWordsOfCulture(string key)
        {
            if (_words.TryGetValue(key, out var words)) return words;
            throw new ArgumentException($"No words for cultur {key}");
        }

    }

}
