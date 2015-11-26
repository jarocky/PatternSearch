namespace PatternSearch.Structures.Suffix
{
  public class FindingResult<T> where T : class
  {
    public T Result { get; set; }

    public int ComparisonsCount { get; set; }
  }
}