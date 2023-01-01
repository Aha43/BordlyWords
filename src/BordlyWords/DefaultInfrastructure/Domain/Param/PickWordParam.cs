using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.DefaultInfrastructure.Domain.Param
{
    public class PickWordParam : IPickWordParam
    {
        public string Culture { get; init; } = "nb-NO";

        public required string Name { get; init; }

        private int? _length = null;
        public int? Length
        {
            get { return _length; }
            init { _length = value.EnsureLegalWordLength(); }
        }
    }
}
