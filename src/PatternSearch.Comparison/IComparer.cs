namespace PatternSearch.Comparison
{
  public interface IComparer
  {
    ComparisonResult Compare(byte[] firstText, byte[] secondText, int minLength);
  }
}