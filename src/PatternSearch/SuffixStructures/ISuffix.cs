namespace PatternSearch.SuffixStructures
{
  public interface ISuffix
  {
    int Initialize();
    SearchResult Find(byte[] pattern);
  }
}