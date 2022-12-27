using BordlyWords.DefaultInfrastructure.Domain;
using BordlyWords.Services;
using BordlyWords.Specification.Api;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest
{
    public class ApiTests
    {
        private IBordlyWordsApi GetApi()
        {
            var services = new ServiceCollection();
            services.AddBordlyWordUsingNsfDictionaryServices();
            var sb = services.BuildServiceProvider();
            return sb.GetRequiredService<IBordlyWordsApi>();
        }

        [Theory]
        [InlineData("danse", true)]
        [InlineData("Danse", true)]
        [InlineData("qanse", false)]
        public async void ShouldCheckWord(string word, bool isWord)
        {
            var api = GetApi();

            var par = new CheckWordParam { Word = word };
            var result = await api.IsWordAsync(par);

            Assert.Equal(isWord, result);
        }
    }
}
