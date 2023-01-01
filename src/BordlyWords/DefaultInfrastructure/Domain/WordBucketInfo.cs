using BordlyWords.Specification.Domain;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class WordBucketInfo : IWordBucketInfo
    {
        public required int Length { get; init; }
        public required int Count { get; init; }
    }
}
