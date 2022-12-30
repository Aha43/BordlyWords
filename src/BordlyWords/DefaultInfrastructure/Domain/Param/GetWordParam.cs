using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Domain.Param
{
    public class GetWordParam : IGetWordParam
    {
        public CultureInfo Culture { get; init; } = new CultureInfo("nb-NO");

        public required string Name { get; init; }

        private int? _length = null;
        public int? Length
        {
            get { return _length; }
            init { _length = value.EnsureLegalWordLength(); }
        }
    }
}
