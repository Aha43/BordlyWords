using BordlyWords.Specification.Domain.Param;

namespace BordlyWords.DefaultInfrastructure
{
    public static class ExtensionMethods
    {
        public static bool MayBeWord(this string s) => s.Length > 0 && s.ToArray().Select(char.IsLetter).Count() == s.Length;

        public static string EnsureMayBeWord(this string word)
        {
            if (word.MayBeWord()) return word;
            throw new ArgumentException($"{word} may not be word");
        }

        public static IEnumerable<string> EnsureOnlyMayBeWords(this IEnumerable<string> words)
        {
            words.ForEach(e => e.MayBeWord());
            return words;
        }

        public static bool IsLegalWordLength(this int? l) => !l.HasValue || l.Value > 0;
        
        public static int? EnsureLegalWordLength(this int? l)
        {
            if (l.IsLegalWordLength()) return l;
            throw new ArgumentException($"{l} is not legal word length");
        }

        public static void ForEach<E>(this IEnumerable<E> elements, Action<E> action)
        {
            foreach (var e in elements) action.Invoke(e);
        }

        public static void EnsureLoadWordsParamParam(this ILoadWordsParam p)
        {
            if (p.MaxWordLength < p.MinWordLength)
            {
                throw new ArgumentException($"{nameof(p)}.{nameof(p.MaxWordLength)} < {nameof(p)}.{nameof(p.MinWordLength)} : {p.MaxWordLength} < {p.MinWordLength}");
            }
        }

        public static string Key(this ICheckWordParam p) => $"{p.Name}_{p.Culture.Name}";

        public static string Key(this IGetWordParam p) => $"{p.Name}_{p.Culture.Name}";

    }

}
