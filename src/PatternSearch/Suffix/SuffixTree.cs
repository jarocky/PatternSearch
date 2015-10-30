using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternSearch.Suffix
{
  public class SuffixTree : ISuffixTree
  {
    private readonly Node _root = new Node();
    private readonly byte[] _text;
    private bool _initialized;

    public int LastFindingComparisonsCount { get; private set; }

    public SuffixTree(byte[] text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      _text = text;
    }

    public void Initialize()
    {
      if (_initialized)
      {
        return;
      }

      for (var i = _text.Length - 1; i >= 0; --i)
      {
        InsertSuffix(_text, i);
      }
      
      _initialized = true;
    }

    private int InsertSuffix(byte[] text, int from)
    {
      var currentComparisonsCount = 0;
      var currentNode = _root;
      for (var i = from; i < text.Length; ++i)
      {
        var character = text[i];
        var findingResult = FindAnyChild(currentNode, character);
        currentComparisonsCount += findingResult.ComparisonsCount;
        if (!findingResult.CharacterExists)
        {
          var n = new Node() { Index = from };
          currentNode.Children.Add(character, n);
        }
        currentNode = currentNode.Children[character];
      }
      currentNode.Children.Add(0, new Node());
      return currentComparisonsCount;
    }

    public IEnumerable<int> Find(byte[] pattern)
    {
      if (!_initialized)
      {
        throw new InvalidOperationException("Tree is not initialized. Use Initialize method before finding.");
      }

      var findingResult = FindNode(pattern);
      LastFindingComparisonsCount = findingResult.ComparisonsCount;
      if (findingResult.Result == null)
      {
        return new List<int>();
      }

      if (findingResult.Result.Children.Count == 1 && findingResult.Result.Children[0].Index == -1)
      {
        return new List<int> {findingResult.Result.Index};
      }

      var indices = VisitTree(findingResult.Result.Children.Values.Where(v => v.Index != -1));

      return indices;
    }

    private FindingResult<Node> FindNode(byte[] pattern)
    {
      var currentComparisonsCount = 0;
      var currentNode = _root;
      for (int i = 0; i < pattern.Length; ++i)
      {
        var character = pattern[i];
        var findingResult = FindAnyChild(currentNode, character);
        currentComparisonsCount += findingResult.ComparisonsCount;
        if (!findingResult.CharacterExists)
        {
          return new FindingResult<Node>
          {
            Result = null,
            ComparisonsCount = currentComparisonsCount
          };
        }

        currentNode = currentNode.Children[character];
      }

      return new FindingResult<Node>
      {
        Result = currentNode,
        ComparisonsCount = currentComparisonsCount
      };
    }

    private static List<int> VisitTree(IEnumerable<Node> children)
    {
      var r = new List<int>();
      foreach (var n1 in children)
      {
        if (n1.Children.ContainsKey(0))
        {
          r.Add(n1.Index);
        }
        
        r.AddRange(VisitTree(n1.Children.Values.Where(v => v.Index != -1)));
      }
      return r;
    }



    private static FindingAnyChildResult FindAnyChild(Node currentNode, byte character)
    {
      var comparisonsCount = 0;
      foreach (var currentCharacter in currentNode.Children.Keys)
      {
        comparisonsCount++;
        if (currentCharacter == character)
        {
          return new FindingAnyChildResult(true, comparisonsCount);
        }
      }

      return new FindingAnyChildResult(false, comparisonsCount);
    }
  }
}