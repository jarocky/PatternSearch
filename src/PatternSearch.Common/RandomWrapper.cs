using System;

namespace PatternSearch.Common
{
  public class RandomWrapper : IRandomWrapper
  {
    private readonly Random _rand = new Random();

    public int Next()
    {
      return _rand.Next();
    }
  }
}