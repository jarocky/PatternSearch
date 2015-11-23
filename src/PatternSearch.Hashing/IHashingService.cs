namespace PatternSearch.Hashing
{
  public interface IHashingService
  {
    long Hash(long[] t);

    long HashRoll(int patternLength, long hash, long firstElementToRemove, long lastElementToAdd);
  }
}