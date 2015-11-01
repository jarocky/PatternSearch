namespace PatternSearch.Suffix
{
  public interface ISuffixTree
  {
    int Initialize();
    SearchResult Find(byte[] pattern);
  }
}