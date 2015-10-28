using System;
using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  public class SuffixTree : ISuffixTree
  {
    private readonly Node _tree = new Node();
    private readonly byte[] _text;
    private bool _initialized;
    private int _buildingTreeComparisonsCount;

    public int LastFindingComparisonsCount { get; private set; }

    public SuffixTree(byte[] text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      _text = text;
    }

    public int Initialize()
    {
      if (_initialized)
      {
        return _buildingTreeComparisonsCount;
      }

      var comparisonsCount = 0;
      for (var i = _text.Length - 1; i >= 0; --i)
      {
        comparisonsCount += InsertSuffix(_text, i);
      }
      
      _initialized = true;
      _buildingTreeComparisonsCount = comparisonsCount;

      return comparisonsCount;
    }

    private int InsertSuffix(byte[] text, int from)
    {
      var currentComparisonsCount = 0;
      var currentNode = _tree;
      for (var i = from; i < text.Length; ++i)
      {
        var character = text[i];
        var findingResult = FindAnyChild(currentNode, character);
        currentComparisonsCount += findingResult.ComparisonsCount;
        if (!findingResult.CharacterExists)
        {
          var n = new Node() { Index = from };
          currentNode.Children.Add(character, n);
          
          return currentComparisonsCount;
        }
        currentNode = currentNode.Children[character];
      }
      
      throw new InvalidOperationException(string.Format("Suffix tree corruption. Text={0}, From={1}", text, from));
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
        yield break;
      }

      foreach (var n2 in VisitTree(findingResult.Result))
      {
        yield return n2.Index;
      }
    }

    private FindingResult<Node> FindNode(byte[] pattern)
    {
      var currentComparisonsCount = 0;
      var currentNode = _tree;
      for (int i = 0; i < pattern.Length; ++i)
      {
        var character = pattern[i];
        var findingResult = FindAnyChild(currentNode, character);
        currentComparisonsCount += findingResult.ComparisonsCount;
        if (!findingResult.CharacterExists)
        {
          for (var j = i; j < pattern.Length; ++j)
          {
            currentComparisonsCount++;
            if (_text[currentNode.Index + j] != pattern[j])
            {
              return new FindingResult<Node>
              {
                Result = null,
                ComparisonsCount = currentComparisonsCount
              };
            }
          }

          return new FindingResult<Node>
          {
            Result = currentNode,
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