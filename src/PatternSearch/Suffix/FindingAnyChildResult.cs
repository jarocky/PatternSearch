namespace PatternSearch.Suffix
{
  internal class FindingAnyChildResult
  {
    public FindingAnyChildResult(bool characterExists, int comparisonsCount)
    {
      ComparisonsCount = comparisonsCount;
      CharacterExists = characterExists;
    }

    public bool CharacterExists { get; private set; }

    public int ComparisonsCount { get; private set; }
  }
}