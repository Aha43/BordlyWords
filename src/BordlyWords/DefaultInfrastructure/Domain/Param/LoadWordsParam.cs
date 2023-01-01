using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.DefaultInfrastructure.Domain.Param
{
    public class LoadWordsParam : ILoadWordsParam
    {
        public int MinWordLength { get; init; } = 1;

        public int MaxWordLength { get; init; } = int.MaxValue;

        public string Culture { get; init; } = "nb-NO";

        public required string Name { get; init; }

        private readonly IEnumerable<string> _words = Enumerable.Empty<string>();
        public required IEnumerable<string> Words
        {
            get => _words;
            init => _words = value.EnsureOnlyMayBeWords();
        }

        private IEnumerable<string>? _checkWords = null;
        public IEnumerable<string>? CheckWords
        {
            get => _checkWords;
            init => _checkWords = value?.EnsureOnlyMayBeWords();
        }

        public string? Description { get; set; }
    }

}
