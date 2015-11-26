using System.IO;
using PatternSearch.Common;
using PatternSearch.Hashing;
using PatternSearch.Search.Brute;
using PatternSearch.Search.RabinKarp;
using PatternSearch.Structures.Suffix;

namespace PatternSearch.Console.Tests
{
  class Program
  {
    static void Main(string[] args)
    {
      var encoder = new ByteStringEncoder();
      var operationTimeTester = new OperationTimeTester();

      var text = File.ReadAllBytes(@"..\doc\pan_wolodyjowski_line_kopia.t");
      var pattern = File.ReadAllBytes(@"..\doc\Pattern_kopia.t");
      var patternString = encoder.GetString(pattern);
      var patternSize = pattern.Length;
      var textSize = text.Length;

      var brute = new BrutePatternSearcher();
      var result = operationTimeTester.Test(brute.Search, pattern, text);
      Show("Pan Wołodyjowski", textSize, "Brute", patternString, patternSize, result);

      Pause();

      var rabinkarpModulo13 = new RabinKarpPatternSearcher(new HashingService(256, 13));
      result = operationTimeTester.Test(rabinkarpModulo13.Search, pattern, text);
      Show("Pan Wołodyjowski", textSize, "Rabin-Karp with hashing modulo 13", patternString, patternSize, result);

      Pause();

      var rabinkarpModulo101 = new RabinKarpPatternSearcher(new HashingService(256, 101));
      result = operationTimeTester.Test(rabinkarpModulo101.Search, pattern, text);
      Show("Pan Wołodyjowski", textSize, "Rabin-Karp with hashing modulo 101", patternString, patternSize, result);

      Pause();

      var rabinkarpModulo2147483647 = new RabinKarpPatternSearcher(new HashingService(256, 2147483647));
      result = operationTimeTester.Test(rabinkarpModulo2147483647.Search, pattern, text);
      Show("Pan Wołodyjowski", textSize, "Rabin-Karp with hashing modulo 2147483647", patternString, patternSize, result);

      Pause();

      var suffixTree = new SuffixTree(text);
      System.Console.Out.WriteLine("Suffix tree building start...");
      var suffixTreeBuildingResult = operationTimeTester.Test(suffixTree.Initialize);
      System.Console.Out.WriteLine("Comparisons count: {0}", suffixTreeBuildingResult.OperationResult);
      System.Console.Out.WriteLine("Suffix tree building finished.");
      System.Console.Out.WriteLine("Elapsed: {0}", suffixTreeBuildingResult.Elapsed);
      System.Console.Out.WriteLine();

      Pause();

      result = operationTimeTester.Test(suffixTree.Find, pattern);
      Show("Pan Wołodyjowski", textSize, "Suffix Tree", patternString, patternSize, result);

      Pause();

      var suffixArray = new SuffixArray(text);
      System.Console.Out.WriteLine("Suffix array building start...");
      var suffixArrayBuildingResult = operationTimeTester.Test(suffixArray.Initialize);
      System.Console.Out.WriteLine("Comparisons count: {0}", suffixArrayBuildingResult.OperationResult);
      System.Console.Out.WriteLine("Suffix array building finished.");
      System.Console.Out.WriteLine("Elapsed: {0}", suffixArrayBuildingResult.Elapsed);
      System.Console.Out.WriteLine();

      Pause();

      result = operationTimeTester.Test(suffixArray.Find, pattern);
      Show("Pan Wołodyjowski", textSize, "Suffix Array", patternString, patternSize, result);

      Pause();
    }

    private static void Pause()
    {
      System.Console.Out.WriteLine("Pause...");
      System.Console.ReadKey();
    }

    private static void Show(
      string textTitle, 
      int textSize,
      string algorithmName, 
      string pattern,
      int patternSize, 
      OperationTimeResult<SearchResult> result)
    {
      System.Console.Out.WriteLine("Text title: {0}, Size: {1}", textTitle, textSize);
      System.Console.Out.WriteLine("Pattern: {0}, Size: {1}", pattern, patternSize);
      System.Console.Out.WriteLine(algorithmName);
      System.Console.Out.WriteLine("Comparisons count: {0}", result.OperationResult.ComparisonsCount);
      System.Console.Out.WriteLine("Occurrences count: {0}", result.OperationResult.OccurrencesCount);
      System.Console.Out.WriteLine("Elapsed: {0}", result.Elapsed);
      System.Console.Out.WriteLine();
    }
  }
}
