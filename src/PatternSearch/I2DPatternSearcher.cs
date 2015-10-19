namespace PatternSearch
{
  public interface I2DPatternSearcher
  {
    SearchResult Search(byte[,] pattern, byte[,] text); 
  }
}