using BordlyWords.DefaultInfrastructure.Api;
using BordlyWords.DefaultInfrastructure.Domain;
using BordlyWords.Specification.Api;
using BordlyWords.Specification.Domain.Param;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace BordlyWords.Services
{
    public static class IoCConfig
    {
        public static IServiceCollection AddBordlyWordUsingNsfDictionaryServices(this IServiceCollection services) 
        {
            return services.AddBordlyWordsServices(opt => 
            {
                opt.AddSource(new WordSource
                {
                    SourceLocation = "./Data/nsf2022.txt"
                });
            });
        }

        public static IServiceCollection AddBordlyWordsServices(this IServiceCollection services, Action<BordlyWordsConfigOpt>? opt)
        {
            var api = new DefaultBordlyWordsApi();

            var o = new BordlyWordsConfigOpt();
            opt?.Invoke(o);

            foreach (var source in o._wordSources)
            {
                Task<ILoadWordsParam> task = ReadWords(source);
                task.Wait();
                api.LoadWordsAsync(task.Result).Wait();
            }

            return services.AddSingleton<IBordlyWordsApi>(api);
        }

        private static async Task<ILoadWordsParam> ReadWords(this WordSource wordSource, CancellationToken cancellationToken = default)
        {
            var text = string.Empty;
            if (wordSource.IsFile) 
            {
                text = File.ReadAllText(wordSource.SourceLocation);
            }
            else
            {
                var uri = new Uri(wordSource.SourceLocation);
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
                text = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            }
            
            var list = new List<string>();
            var reader = new StringReader(text);
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                var word = wordSource.ToWord(line);
                list.Add(word);
            }

            return new LoadWordsParam
            {
                Culture = wordSource.Culture,
                Words = list.AsEnumerable()
            };
        }
    }

    public class BordlyWordsConfigOpt
    {
        internal readonly List<WordSource> _wordSources = new();
        public BordlyWordsConfigOpt AddSource(WordSource wordSource) 
        {
            _wordSources.Add(wordSource);
            return this;
        }
    }

    public class WordSource
    {
        public required string SourceLocation { get; set; }
        public bool IsFile { get; set; } = true;
        public CultureInfo Culture { get; set; } = new CultureInfo("nb-NO");
        public Func<string, string> ToWord { get; set; } = e => e.ToString();
    }
}
