namespace BordlyWords.Specification.Domain
{
    public interface IWordDictionaryInfo
    {
        string Name { get; }
        string Culture { get; }
        string? Description { get; }
        int WordCount { get; }
        int? WordCountForCheck { get; }
        IEnumerable<IWordBucketInfo> BucketInfo { get; }
    }
}
