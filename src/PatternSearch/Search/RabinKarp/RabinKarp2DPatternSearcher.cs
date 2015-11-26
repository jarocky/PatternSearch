using System;
using System.Collections.Generic;
using System.Linq;
using PatternSearch.Hashing;

namespace PatternSearch.Search.RabinKarp
{
  public class RabinKarp2DPatternSearcher : I2DPatternSearcher
  {
    private readonly IHashingService _hashingService;

    public RabinKarp2DPatternSearcher(IHashingService hashingService)
    {
      _hashingService = hashingService;
    }

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

      var patternHash = GetPatternHash(pattern);
      var textHashArray = GetTextHashArray(pattern, text);

      var indices = new List<int>();
      var comparisonsCount = 0;
      long hSubTextHash = 0;
      var textHLineHashes = new List<long>();
      for (var i = 0; i < textHashArray.GetLength(0); i++)
      {
        for (var j = 0; j <= textHashArray.GetLength(1) - pattern.GetLength(1); j++)
        {
          if (j == 0)
          {
            for (var k = 0; k < pattern.GetLength(1); k++)
            {
              textHLineHashes.Add(textHashArray[i, k]);
            }
            hSubTextHash = _hashingService.Hash(textHLineHashes.ToArray());
            textHLineHashes.Clear();
          }

          if (j + pattern.GetLength(1) <= textHashArray.GetLength(1) && j != 0)
          {
            hSubTextHash = _hashingService.HashRoll(pattern.GetLength(1), hSubTextHash, textHashArray[i, j - 1], textHashArray[i, j - 1 + pattern.GetLength(1)]);
          }

          comparisonsCount++;
          if (patternHash == hSubTextHash)
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
      }

      return new SearchResult
      {
        ComparisonsCount = comparisonsCount,
        Indices = indices.ToArray()
      };
    }

    private long[,] GetTextHashArray(byte[,] pattern, byte[,] text)
    {
      var hashTextTable = new long[text.GetLength(0) - pattern.GetLength(0) + 1, text.GetLength(1)];
      for (var j = 0; j < text.GetLength(1); j++)
      {
        var subTextHash = SubTextHash(pattern, text, j);
        hashTextTable[0, j] = subTextHash;
        for (int r = 0; r < text.GetLength(0) - pattern.GetLength(0); r++)
        {
          subTextHash = _hashingService.HashRoll(pattern.GetLength(0), hashTextTable[r, j], text[r, j], text[r + pattern.GetLength(0), j]);
          hashTextTable[r + 1, j] = subTextHash;
        }
      }
      return hashTextTable;
    }

    private long GetPatternHash(byte[,] pattern)
    {
      var patternLineHashes = new List<long>();
      for (var j = 0; j < pattern.GetLength(1); j++)
      {
        var subTextHash = SubTextHash(pattern, pattern, j);
        patternLineHashes.Add(subTextHash);
      }
      var patternHash = _hashingService.Hash(patternLineHashes.ToArray());
      return patternHash;
    }

    private long SubTextHash(byte[,] pattern, byte[,] text, int j)
    {
      var hashText = new byte[pattern.GetLength(0)];
      for (var i = 0; i < pattern.GetLength(0); i++)
      {
        hashText[i] = text[i, j];
      }
      var subTextHash = _hashingService.Hash(hashText.Select(e => (long)e).ToArray());
      return subTextHash;
    }
  }
}