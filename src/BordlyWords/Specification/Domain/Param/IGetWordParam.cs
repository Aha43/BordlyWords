using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface IGetWordParam
    {
        string Name { get; }
        string Culture { get; }
        int? Length { get; }
    }
}
