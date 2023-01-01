using BordlyWords.Specification.Domain;
using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.Specification.Api
{
    public interface IBordlyWordsApi
    {
        public Task LoadWordsAsync(ILoadWordsParam p, CancellationToken cancellationToken = default);
        public Task<IPickedWord> Pick(IPickWordParam p, CancellationToken cancellationToken = default);
        public Task<IWordCheck> Check(ICheckWordParam p, CancellationToken cancellationToken = default);
        public Task<IEnumerable<IWordDictionaryInfo>> GetInfo(CancellationToken cancellationToken = default);
    }
}
