using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  public interface ISuffixTree
  {
    int LastFindingComparisonsCount { get; }
    void Initialize();
    IEnumerable<int> Find(byte[] pattern);
  }
}