using System;
using System.Collections.Generic;
using System.Linq;
using PatternSearch.Hashing;

namespace PatternSearch.Search.RabinKarp
{
  public class RabinKarpPatternSearcher : IPatternSearcher
  {
    private readonly IHashingService _hashingService;

    public RabinKarpPatternSearcher(IHashingService hashingService)
    {
      _hashingService = hashingService;
    }

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

      var indices = new List<int>();
      var comparisonsCount = 0;
      var patternHash = _hashingService.Hash(pattern.Select(e => (long)e).ToArray());
      var subTextHash = _hashingService.Hash(text.Select(e => (long)e).Take(pattern.Length).ToArray());
      for (var i = 0; i <= text.Length - pattern.Length; i++)
      {
        comparisonsCount++;
        if (patternHash == subTextHash)
        {
          for (var j = 0; j < pattern.Length; j++)
          {
            comparisonsCount++;
            if (pattern[j] != text[i + j])
            {
              break;
            }

            if (j == pattern.Length - 1)
            {
              indices.Add(i);
            }
          }
        }
        if (i + pattern.Length < text.Length)
        {
          subTextHash = _hashingService.HashRoll(pattern.Length, subTextHash, text[i], text[i + pattern.Length]);
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