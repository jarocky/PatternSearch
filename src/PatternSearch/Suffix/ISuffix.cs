namespace PatternSearch.Suffix
{
  public interface ISuffix
  {
    int Initialize();
    SearchResult Find(byte[] pattern);
  }
}