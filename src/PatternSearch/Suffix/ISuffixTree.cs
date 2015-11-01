using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  public interface ISuffixTree
  {
    int LastFindingComparisonsCount { get; }
    int Initialize();
    IEnumerable<int> Find(byte[] pattern);
  }
}