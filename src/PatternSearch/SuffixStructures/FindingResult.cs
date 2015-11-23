namespace PatternSearch.SuffixStructures
{
  public class FindingResult<T> where T : class
  {
    public T Result { get; set; }

    public int ComparisonsCount { get; set; }
  }
}