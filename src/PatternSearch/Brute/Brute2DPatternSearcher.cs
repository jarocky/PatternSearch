using System;
using System.Collections.Generic;

namespace PatternSearch.Brute
{
  public class Brute2DPatternSearcher : I2DPatternSearcher
  {
    public SearchResult Search(byte[,] pattern, byte[,] text)
    {
      if (pattern == null)
      {
        throw new ArgumentNullException("pattern", "Cannot be null");
      }

      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      if (pattern.Length == 0 || text.Length == 0 || pattern.GetLength(0) > text.GetLength(0) || pattern.GetLength(1) > text.GetLength(1))
      {
        return new SearchResult
        {
          ComparisonsCount = 0,
          Indices = new int[0]
        };
      }

      var indices = new List<int>();
      var comparisonsCount = 0;
      for (var j = 0; j <= text.GetLength(1) - pattern.GetLength(1); j++)
      {
        for (var i = 0; i <= text.GetLength(0) - pattern.GetLength(0); i++)
        {
          var hitCount = 0;
          for (var y = 0; y < pattern.GetLength(1); y++)
          {
            for (var x = 0; x < pattern.GetLength(0); x++)
            {
              comparisonsCount++;
              if (pattern[x, y] == text[i + x, j + y])
              {
                hitCount++;
              }

              if (hitCount == pattern.GetLength(0) * pattern.GetLength(1))
              {
                indices.Add(j * text.GetLength(0) + i);
              }
            }
          }
        }
      }

      return new SearchResult
      {
        ComparisonsCount = comparisonsCount,
        Indices = indices.ToArray()
      };
    }
  }
}