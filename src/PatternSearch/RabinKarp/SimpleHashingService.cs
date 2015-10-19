using System;

namespace PatternSearch.RabinKarp
{
  public class SimpleHashingService : IHashingService
  {
    public long Hash(long[] t)
    {
      long sum = 0;
      for (var i = 0; i < t.Length; i++)
      {
        sum += t[i] * (long)Math.Pow(101, i);
      }

      return sum;
    }

    public long HashRoll(int patternLength, long hash, long byteToRemove, long byteToAdd)
    {
      return (hash - byteToRemove) / 101 + byteToAdd * (long)Math.Pow(101, patternLength - 1);
    }
  }
}