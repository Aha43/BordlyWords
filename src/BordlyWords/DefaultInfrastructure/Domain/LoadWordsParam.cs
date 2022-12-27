using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class LoadWordsParam : ILoadWordsParam
    {
        public CultureInfo Culture { get; init; } = new CultureInfo("nb-NO");

        private readonly IEnumerable<string> _words = Enumerable.Empty<string>();
        public required IEnumerable<string> Words 
        {
            get => _words;
            init => _words = value.EnsureOnlyMayBeWords();
        }

    }

}
