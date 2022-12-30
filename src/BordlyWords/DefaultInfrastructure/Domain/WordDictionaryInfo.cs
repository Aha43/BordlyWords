using BordlyWords.Specification.Domain;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class WordDictionaryInfo : IWordDictionaryInfo
    {
        public required string Name { get; init; }
        public required string Culture { get; init; }
        public required int WordCount { get; init; }
    }
}
