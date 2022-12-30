using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface ILoadWordsParam
    {
        int MinWordLength { get; }
        int MaxWordLength { get; }
        CultureInfo Culture { get; }
        string Name { get; }
        string? Description { get; }
        IEnumerable<string> Words { get; }
        IEnumerable<string>? CheckWords { get; }
    }
}
