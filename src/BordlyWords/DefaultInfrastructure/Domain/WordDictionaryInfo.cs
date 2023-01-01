using BordlyWords.Specification.Domain;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class WordDictionaryInfo : IWordDictionaryInfo
    {
        public required string Name { get; init; }
        public required string Culture { get; init; }
        public string? Description { get; init; }
        public required int WordCount { get; init; }
        public int? WordCountForCheck { get; init; }
        public required IEnumerable<IWordBucketInfo> BucketInfo { get; init; }
    }
}
