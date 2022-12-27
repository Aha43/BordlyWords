using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface ICheckWordParam
    {
        public CultureInfo Culture { get; }
        public string Word { get; init; }
    }
}
