using System;
using System.Collections.Generic;

namespace PatternSearch.Search.Brute
{
  public class BrutePatternSearcher : IPatternSearcher
  {
    public SearchResult Search(byte[] pattern, byte[] text)
    {
      if (pattern == null)
      {
        throw new ArgumentNullException("pattern", "Cannot be null");
      }

      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      if (pattern.Length == 0 || text.Length == 0 || pattern.Length > text.Length)
      {
        return new SearchResult
        {
          ComparisonsCount = 0,
          Indices = new int[0]
        };
      }

      var patternIndex = 0;
      var textIndex = 0;
      var indices = new List<int>();
      var comparisonsCount = 0;
      while (textIndex <= text.Length - pattern.Length)
      {
        while (patternIndex < pattern.Length)
        {
          comparisonsCount++;
          if (pattern[patternIndex] == text[textIndex + patternIndex])
          {
            if (patternIndex == pattern.Length - 1)
            {
              indices.Add(textIndex);
              patternIndex = 0;
              break;
            }
            patternIndex++;
          }
          else
          {
            patternIndex = 0;
            break;
          }
        }
        textIndex++;
      }

      return new SearchResult
      {
        ComparisonsCount = comparisonsCount,
        Indices = indices.ToArray()
      };
    }
  }
}