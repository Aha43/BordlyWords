using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface ILoadWordsParam
    {
        public int MinWordLength { get; }
        public int MaxWordLength { get; }
        public CultureInfo Culture { get; }
        public string Name { get; }
        public string? Description { get; }
        public IEnumerable<string> Words { get; }
        public IEnumerable<string>? CheckWords { get; }
    }
}
