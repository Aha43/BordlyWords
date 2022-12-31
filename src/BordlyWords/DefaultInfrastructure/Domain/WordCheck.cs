using BordlyWords.Specification.Domain;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class WordCheck : IWordCheck
    {
        public required string Word { get; init; }
        public required bool IsWord { get; init; }
    }
}
