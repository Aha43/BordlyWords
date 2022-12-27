using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BordlyWords.Specification.Domain.Param
{
    public interface IGetWordParam
    {
        public CultureInfo Culture { get; }
        public int? Length { get; }
    }
}
