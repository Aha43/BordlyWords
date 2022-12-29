using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class CheckWordParam : ICheckWordParam
    {
        public CultureInfo Culture { get; init; } = new CultureInfo("nb-NO");

        public required string Name { get; init; }

        private string _word = string.Empty;
        public required string Word 
        {
            get => _word;
            init => _word = value.EnsureMayBeWord();
        }
    }

}
