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
                opt.AddSource(new WordDictionary
                {
                    Name = "NSF2022",
                    WordSource = new WordSource
                    { 
                        SourceLocation = "./Data/nsf2022.txt"
                    } 
                }); 
            });
        }

        public static IServiceCollection AddBordlyWordsServices(this IServiceCollection services, Action<BordlyWordsConfigOpt>? opt)
        {
            var api = new DefaultBordlyWordsApi();

            var o = new BordlyWordsConfigOpt();
            opt?.Invoke(o);

            foreach (var dictionary in o._dictionaries)
            {
                Task<ILoadWordsParam> task = CreateLoadWordsParamAsync(dictionary);
                task.Wait();
                api.LoadWordsAsync(task.Result).Wait();
            }

            return services.AddSingleton<IBordlyWordsApi>(api);
        }

        private static async Task<ILoadWordsParam> CreateLoadWordsParamAsync(this WordDictionary dictionary, CancellationToken cancellationToken = default)
        {
            var words = await dictionary.WordSource.ReadWordsAsync(cancellationToken).ConfigureAwait(false);
            var checkWords = default(IEnumerable<string>);
            if (dictionary.CheckWordSource != null)
            {
                checkWords = await dictionary.CheckWordSource.ReadWordsAsync(cancellationToken).ConfigureAwait(false);
            }

            return new LoadWordsParam
            {
                Culture = dictionary.Culture,
                Words = words,
                CheckWords = checkWords
            };
        }

        public static async Task<IEnumerable<string>> ReadWordsAsync(this WordSource source, CancellationToken cancellationToken = default)
        {
            var text = string.Empty;
            if (source.IsFile)
            {
                text = File.ReadAllText(source.SourceLocation);
            }
            else
            {
                var uri = new Uri(source.SourceLocation);
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
                var word = source.ToWord(line);
                list.Add(word);
            }

            return list;
        }

    }

    public class BordlyWordsConfigOpt
    {
        internal readonly List<WordDictionary> _dictionaries = new();
        public BordlyWordsConfigOpt AddSource(WordDictionary dictionary) 
        {
            _dictionaries.Add(dictionary);
            return this;
        }
    }

    public class WordDictionary
    {
        public CultureInfo Culture { get; init; } = new CultureInfo("nb-NO");
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required WordSource WordSource { get; init; }
        public WordSource? CheckWordSource { get; init; }
    }

    public class WordSource
    {
        public required string SourceLocation { get; set; }
        public bool IsFile { get; set; } = true;
        public Func<string, string> ToWord { get; set; } = e => e.ToString();
    }
}
