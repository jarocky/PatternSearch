using System.Collections.Generic;
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
      var operationTimeTester = new OperationTimeTester();

      var list = new List<int>() {19, 29, 1, 23, 43, 3, 6, 8, 12, 9};

      var resultSkipListInsert = operationTimeTester.Test(InsertIntoSkipList, list);
      System.Console.Out.WriteLine("Elapsed: {0}", resultSkipListInsert.Elapsed);
      var resultSkipListFind = operationTimeTester.Test(resultSkipListInsert.OperationResult.Find, 12);
      System.Console.Out.WriteLine("Find result: {0}", resultSkipListFind.OperationResult.Result);
      System.Console.Out.WriteLine("Comparisons: {0}", resultSkipListFind.OperationResult.ComparisonsCount);
      System.Console.Out.WriteLine("Elapsed: {0}", resultSkipListFind.Elapsed);
      System.Console.Out.WriteLine("Remove");
      var resultSkipListRemove = operationTimeTester.Test(resultSkipListInsert.OperationResult.Remove, 12);
      System.Console.Out.WriteLine("Comparisons: {0}", resultSkipListRemove.OperationResult);
      System.Console.Out.WriteLine("Elapsed: {0}", resultSkipListRemove.Elapsed);
      System.Console.Out.WriteLine();
      System.Console.Out.WriteLine();
      var resultTreeInsert = operationTimeTester.Test(InsertIntoTree, list);
      System.Console.Out.WriteLine("Elapsed: {0}", resultTreeInsert.Elapsed);
      var resultTreeFind = operationTimeTester.Test(resultTreeInsert.OperationResult.Find, 12);
      System.Console.Out.WriteLine("Find result: {0}", resultTreeFind.OperationResult.Result.Value == 12);
      System.Console.Out.WriteLine("Comparisons: {0}", resultTreeFind.OperationResult.ComparisonsCount);
      System.Console.Out.WriteLine("Elapsed: {0}", resultTreeFind.Elapsed);
      System.Console.Out.WriteLine("Remove");
      var resultTreeRemove = operationTimeTester.Test(resultTreeInsert.OperationResult.Remove, 12);
      System.Console.Out.WriteLine("Comparisons: {0}", resultTreeRemove.OperationResult);
      System.Console.Out.WriteLine("Elapsed: {0}", resultTreeRemove.Elapsed);

      System.Console.ReadKey();
    }

    public static SkipList<int> InsertIntoSkipList(List<int> list)
    {
      var skipList = new SkipList<int>(new RandomWrapper());
      var comparisons = 0;
      foreach (var n in list)
      {
        comparisons += skipList.Insert(n);
      }
      System.Console.Out.WriteLine("SkipList");
      System.Console.Out.WriteLine("Insert");
      System.Console.Out.WriteLine("Comparisons: {0}", comparisons);
      return skipList;
    }

    public static BinaryTree<int> InsertIntoTree(List<int> list)
    {
      var tree = new BinaryTree<int>();
      var comparisons = 0;
      foreach (var n in list)
      {
        comparisons += tree.Insert(n);
      }
      System.Console.Out.WriteLine("Tree");
      System.Console.Out.WriteLine("Insert");
      System.Console.Out.WriteLine("Comparisons: {0}", comparisons);
      return tree;
    }
  }
}











//FileStream stream = File.OpenRead(@"..\doc\pan_wolodyjowski_line.t");
//int myByte;
//string word = "";
//while ((myByte = stream.ReadByte()) != -1)
//{
//  var inChar = encoder.GetString((byte)myByte);
//  if (inChar != " " && inChar != "\n" && inChar != "\r")
//  {
//    word += inChar;
//  }
//  else
//  {
//    skipList.Insert(word);
//    tree.Insert(word);
//    word = "";
//  }
//}