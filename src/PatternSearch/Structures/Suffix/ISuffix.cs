namespace PatternSearch.Structures.Suffix
{
  public interface ISuffix
  {
    int Initialize();
    SearchResult Find(byte[] pattern);
  }
}