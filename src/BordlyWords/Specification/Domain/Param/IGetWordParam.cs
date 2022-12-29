using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface IGetWordParam
    {
        string Name { get; }
        CultureInfo Culture { get; }
        int? Length { get; }
    }
}
