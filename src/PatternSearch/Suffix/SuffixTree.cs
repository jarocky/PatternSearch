using System;
using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  public class SuffixTree
  {
    private readonly Node _tree = new Node();
    private readonly string _text;

    public SuffixTree(string text)
    {
      _text = text;
      for (var i = text.Length - 1; i >= 0; --i)
      {
        InsertSuffix(text, i);
      }
    }

    private void InsertSuffix(string text, int from)
    {
      var currentNode = _tree;
      for (var i = from; i < text.Length; ++i)
      {
        var character = text[i];
        if (!currentNode.Children.ContainsKey(character))
        {
          var n = new Node() { Index = from };
          currentNode.Children.Add(character, n);
          return;
        }
        currentNode = currentNode.Children[character];
      }
      throw new InvalidOperationException(string.Format("Suffix tree corruption. Text={0}, From={1}", text, from));
    }

    public IEnumerable<int> Find(string pattern)
    {
      var n = FindNode(pattern);
      if (n == null)
      {
        yield break;
      }
      foreach (var n2 in VisitTree(n))
      {
        yield return n2.Index;
      }
    }

    private Node FindNode(string s)
    {
      var currentNode = _tree;
      for (int i = 0; i < s.Length; ++i)
      {
        var character = s[i];
        if (!currentNode.Children.ContainsKey(character))
        {
          for (var j = i; j < s.Length; ++j)
          {
            if (_text[currentNode.Index + j] != s[j])
            {
              return null;
            }
          }
          return currentNode;
        }
        currentNode = currentNode.Children[character];
      }
      return currentNode;
    }

    private static IEnumerable<Node> VisitTree(Node n)
    {
      foreach (var n1 in n.Children.Values)
      {
        foreach (var n2 in VisitTree(n1))
        {
          yield return n2;
        }
      }
      yield return n;
    }
  }
}