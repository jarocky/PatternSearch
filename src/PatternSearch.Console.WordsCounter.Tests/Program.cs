using System.IO;
using System.Text;
using PatternSearch.Common;
using PatternSearch.Hashing;
using PatternSearch.Structures.Hashing;

namespace PatternSearch.Console.WordsCounter.Tests
{
  class Program
  {
    static void Main(string[] args)
    {
      var encoder = new ByteStringEncoder();
      var hashingArray = new HashingArray(new HashingService(256, 100000003), 100000003);

      FileStream stream = File.OpenRead(@"..\doc\pan_wolodyjowski_line.t");
      int myByte;
      string word = "";
      while ((myByte = stream.ReadByte()) != -1)
      {
        var inChar = encoder.GetString((byte)myByte);
        if (inChar != " " && inChar != "\n" && inChar != "\r")
        {
          word += inChar;
        }
        else
        {
          hashingArray.Add(word);
          word = "";
        }
      }

      var sb = new StringBuilder();
      using (var file = System.IO.File.AppendText(@"..\doc\words_count.t"))
      {
        foreach (var wordList in hashingArray)
        {
          foreach (var w in wordList)
          {
            sb.AppendLine(string.Format("Word: {0}, count: {1}",
            w.Value,
            w.Count));
            file.Write(sb.ToString());
            sb.Clear();
          }
        }
      }
    }
  }
}