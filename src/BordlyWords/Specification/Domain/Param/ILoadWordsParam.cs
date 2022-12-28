using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface ILoadWordsParam
    {
        public CultureInfo Culture { get; init; }
        public IEnumerable<string> Words { get; init; }
        public IEnumerable<string>? CheckWords { get; init; }
    }
}
