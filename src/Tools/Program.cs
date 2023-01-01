// See https://aka.ms/new-console-template for more information
using BordlyWords.Utilities;
using CommandLine;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await WashKorpus10000Async().ConfigureAwait(false);
    }

    private static async Task WashKorpus10000Async()
    {
        var nsfWords = await ReadWriteUtilities.ReadWordsAsync("./Data/nsf2022.txt").ConfigureAwait(false);
        var korpus10000Words = await ReadWriteUtilities.ReadWordsAsync("./Data/korpus.uib.no_humfak_nta_ord10000.txt", ReadWriteUtilities.SecondTokenIsWordLineToWords).ConfigureAwait(false);

        var accepted = new List<string>();
        var rejected = new List<string>();
        foreach (var word in korpus10000Words)
        {
            if (Array.BinarySearch(nsfWords, word) > -1)
            { 
                accepted.Add(word); 
            }
            else
            {
                rejected.Add(word);
            }
        }

        await ReadWriteUtilities.WriteWordsAsync("c:\\temp\\korpus.uib.no_humfak_nta_ord10000.nfs2022Accepted.txt", accepted).ConfigureAwait(false);
        await ReadWriteUtilities.WriteWordsAsync("c:\\temp\\korpus.uib.no_humfak_nta_ord10000.nfs2022Rejected.txt", rejected).ConfigureAwait(false);
    }

}

public class Options
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool WashKorpus10000 { get; set; }
}


