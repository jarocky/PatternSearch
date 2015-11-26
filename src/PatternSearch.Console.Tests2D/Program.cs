using System.IO;
using PatternSearch.Common;
using PatternSearch.Hashing;
using PatternSearch.Search.Brute;
using PatternSearch.Search.RabinKarp;

namespace PatternSearch.Console.Tests2D
{
  class Program
  {
    static void Main(string[] args)
    {
      const int textVLen = 100;
      const int patternVLen = 2;

      var encoder = new ByteStringEncoder();
      var operationTimeTester = new OperationTimeTester();
      
      var text = GetByte2DArray(@"..\doc\Text2D.t", textVLen);
      var pattern = GetByte2DArray(@"..\doc\Pattern2D.t", patternVLen);
      var patternString = encoder.GetStringTo2DArray(pattern);
      var patternSize = string.Format("{0}x{1}", pattern.GetLength(0), pattern.GetLength(1));
      var textSize = string.Format("{0}x{1}", text.GetLength(0), text.GetLength(1));

      var brute = new Brute2DPatternSearcher();
      var result = operationTimeTester.Test(brute.Search, pattern, text);
      Show("Text", textSize, "Brute", patternString, patternSize, result);

      var rabinkarp2DModulo13 = new RabinKarp2DPatternSearcher(new HashingService(256, 13));
      result = operationTimeTester.Test(rabinkarp2DModulo13.Search, pattern, text);
      Show("Text", textSize, "Rabin-Karp 2D with hashing modulo 13", patternString, patternSize, result);

      var rabinkarp2DModulo101 = new RabinKarp2DPatternSearcher(new HashingService(256, 101));
      result = operationTimeTester.Test(rabinkarp2DModulo101.Search, pattern, text);
      Show("Text", textSize, "Rabin-Karp 2D with hashing modulo 101", patternString, patternSize, result);

      var rabinkarp2DModulo2147483647 = new RabinKarp2DPatternSearcher(new HashingService(256, 2147483647));
      result = operationTimeTester.Test(rabinkarp2DModulo2147483647.Search, pattern, text);
      Show("Text", textSize, "Rabin-Karp 2D with hashing modulo 2147483647", patternString, patternSize, result);
    }

    private static void Show(
      string textTitle,
      string textSize,
      string algorithmName,
      string pattern,
      string patternSize,
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
    
    private static byte[,] GetByte2DArray(string fullFileName, int k)
    {
      using (var fs = new FileStream(fullFileName, FileMode.Open, FileAccess.Read))
      {
        var v = fs.Length / k;
        var array = new byte[k, v];
        for (var j = 0; j < v; j++)
        {
          for (var i = 0; i < k; i++)
          {
            var b = fs.ReadByte();
            array[i, j] = (byte)b;
          }
        }
        return array;
      }
    }
  }
}