namespace PatternSearch
{
  public interface IPatternSearcher
  {
    SearchResult Search(byte[] pattern, byte[] text);
  }
}