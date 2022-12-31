using BordlyWords.Specification.Domain;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class PickedWord : IPickedWord
    {
        public string? Word { get; init; }
    }
}
