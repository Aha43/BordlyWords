using BordlyWords.DefaultInfrastructure.Api;
using BordlyWords.DefaultInfrastructure.Domain.Param;
using BordlyWords.Specification.Api;
using BordlyWords.Specification.Domain.Param;
using BordlyWords.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace BordlyWords.Services
{
    public static class IoCConfig
    {
        public static IServiceCollection AddBordlyWordNorwegianServices(this IServiceCollection services) 
        {
            return services.AddBordlyWordsServices(opt => 
            {
                opt.AddSource(new WordDictionary
                {
                    Name = "NSF2022",
                    Description = "Pick and check words using 'Norwegian Scrabble Forbund' word list",
                    WordSource = new WordSource
                    {
                        SourceLocation = "./Data/nsf2022.txt"
                    }
                });
                opt.AddSource(new WordDictionary
                {
                    Name = "CommonAndNSF2022",
                    Description = "Pick from a list of common Norwegian words but check using 'Norwegian Scrabble Forbund' word list",
                    WordSource = new WordSource
                    { 
                        SourceLocation = "./Data/korpus.uib.no_humfak_nta_ord10000.nfs2022Accepted.txt"
                    },
                    CheckWordSource = new WordSource
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
                Name = dictionary.Name,
                Description = dictionary.Description,
                Words = words,
                CheckWords = checkWords
            };
        }

        private static async Task<IEnumerable<string>> ReadWordsAsync(this WordSource source, CancellationToken cancellationToken = default)
        {
            if (source.IsFile)
            {
                return await ReadWriteUtilities.ReadWordsAsync(source.SourceLocation, source.ToWord, cancellationToken).ConfigureAwait(false);
            }

            var uri = new Uri(source.SourceLocation);
            return await ReadWriteUtilities.ReadWordsAsync(uri, source.ToWord, cancellationToken).ConfigureAwait(false);
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
        public string Culture { get; init; } = "nb-NO";
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
