namespace PatternSearch
{
  public class SearchResult
  {

    public int ComparisonsCount { get; set; }

    public int[] Indices { get; set; }

    public int OccurrencesCount
    {
      get
      {
        if (Indices == null)
        {
          return 0;
        }

        return Indices.Length;
      }
    }
  }
}