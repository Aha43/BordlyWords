using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.Specification.Api
{
    public interface IBordlyWordsApi
    {
        public Task LoadWordsAsync(ILoadWordsParam p, CancellationToken cancellationToken = default);
        public Task<string?> GetWordAsync(IGetWordParam p, CancellationToken cancellationToken = default);
        public Task<bool> IsWordAsync(ICheckWordParam p, CancellationToken cancellationToken = default);
    }
}
