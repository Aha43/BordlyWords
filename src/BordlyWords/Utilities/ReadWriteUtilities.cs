namespace BordlyWords.Utilities
{
    public static class ReadWriteUtilities
    {
        public static Func<string, string> LineIsWordLineToWords => l => l;

        private static readonly char[] _sep = { ' ' };
        public static Func<string, string> SecondTokenIsWordLineToWords => l => l.Split(_sep, StringSplitOptions.RemoveEmptyEntries)[1];

        public static async Task<string[]> ReadWordsAsync(string path, Func<string, string>? LineToWords = null, CancellationToken cancellationToken = default)
        {
            var text = await File.ReadAllTextAsync(path, cancellationToken).ConfigureAwait(false);
            return ParseWords(text, LineToWords);
        }

        public static async Task<string[]> ReadWordsAsync(Uri uri, Func<string, string>? LineToWords = null, CancellationToken cancellationToken = default)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            var text = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return ParseWords(text, LineToWords);
        }

        public static string[] ParseWords(string text, Func<string, string>? LineToWords = null)
        {
            LineToWords ??= LineIsWordLineToWords;

            var list = new List<string>();
            var reader = new StringReader(text);
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                var word = LineToWords(line);
                word = word.Trim().ToLower();
                list.Add(word);
            }

            list.Sort();

            return list.ToArray();
        }

        public static async Task WriteWordsAsync(string path, IEnumerable<string> words, CancellationToken cancellationToken = default)
        {
            using StreamWriter writer = new(path);
            foreach (var word in words)
            {
                if (word != null)
                {
                    await writer.WriteLineAsync(word).ConfigureAwait(false);
                }
            }
        }

    }

}
