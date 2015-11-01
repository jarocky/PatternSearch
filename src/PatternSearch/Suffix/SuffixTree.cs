using System;
using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  public class SuffixTree : ISuffixTree
  {
    private readonly Node _root = new Node();
    private readonly byte[] _text;
    private bool _initialized;
    private int _buildingComparisonsCount;

    public SuffixTree(byte[] text)
    {
      if (text == null)
      {
        throw new ArgumentNullException(nameof(text), "Cannot be null");
      }

      _text = text;
    }

    public int Initialize()
    {
      if (_initialized)
      {
        return _buildingComparisonsCount;
      }

      var currentComparisons = 0;
      for (var i = _text.Length - 1; i >= 0; --i)
      {
        currentComparisons += InsertSuffix(_text, i);
      }
      
      _initialized = true;
      _buildingComparisonsCount = currentComparisons;

      return _buildingComparisonsCount;
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

    public SearchResult Find(byte[] pattern)
    {
      if (!_initialized)
      {
        throw new InvalidOperationException("Tree is not initialized. Use Initialize method before finding.");
      }

      var results = new List<int>();
      var findingResult = FindNode(pattern);
      if (findingResult.Result == null)
      {
        return new SearchResult
        {
          Indices = results.ToArray(),
          ComparisonsCount = findingResult.ComparisonsCount
        };
      }

      if (findingResult.Result.Children.ContainsKey(0))
      {
        results.Add(findingResult.Result.Index);

        if (findingResult.Result.Children.Count == 1)
        {
          return new SearchResult
          {
            Indices = results.ToArray(),
            ComparisonsCount = findingResult.ComparisonsCount
          };
        }
      }

      var indices = VisitTree(findingResult.Result.Children.Values);
      results.AddRange(indices);

      return new SearchResult
      {
        Indices = results.ToArray(),
        ComparisonsCount = findingResult.ComparisonsCount
      };
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
        
        r.AddRange(VisitTree(n1.Children.Values));
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