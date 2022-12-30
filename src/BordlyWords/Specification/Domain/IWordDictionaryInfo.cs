namespace BordlyWords.Specification.Domain
{
    public interface IWordDictionaryInfo
    {
        string Name { get; }
        string Culture { get; }
        int WordCount { get; }
    }
}
