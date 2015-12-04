using System.IO;
using PatternSearch.Common;
using PatternSearch.Structures.Lists;
using PatternSearch.Structures.Trees;

namespace PatternSearch.Console.Structures.Tests
{
  class Program
  {
    static void Main(string[] args)
    {
      var encoder = new ByteStringEncoder();
      var skipList = new SkipList<string>(new RandomWrapper());
      var tree = new BinaryTree<string>();

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
          skipList.Insert(word);
          tree.Insert(word);
          word = "";
        }
      }
    }
  }
}
