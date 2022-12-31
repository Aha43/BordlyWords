using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface ICheckWordParam
    {
        string Culture { get; }
        string Name { get; }
        string Word { get; init; }
    }
}
