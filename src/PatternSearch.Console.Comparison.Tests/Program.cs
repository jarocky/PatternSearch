using System.IO;
using System.Text;
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

      var sb = new StringBuilder();
      sb.AppendLine(string.Format("Max length: {0}\n", result.MaxLength));
      using (var file = System.IO.File.AppendText(@"..\doc\pie.t"))
      {
        file.Write(sb.ToString());
        sb.Clear();
        foreach (var index in result.Indices)
        {
          sb.AppendLine(string.Format("Length: {0}, pi_index: {1}, e_index: {2}",
            index.Value,
            index.Key.Item1,
            index.Key.Item2));
          file.Write(sb.ToString());
          sb.Clear();
        }
      }
    }
  }
}
