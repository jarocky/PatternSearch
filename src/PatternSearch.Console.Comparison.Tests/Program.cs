using System.IO;
using PatternSearch.Comparison;

namespace PatternSearch.Console.Comparison.Tests
{
  class Program
  {
    static void Main(string[] args)
    {
      var text1 = File.ReadAllBytes(@"..\doc\pi.t");
      var text2 = File.ReadAllBytes(@"..\doc\e.t");

      var comparer = new Comparer();
      var k = 5;

      var result = comparer.Compare(text1, text2, k);
    }
  }
}
