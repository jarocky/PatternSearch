using System;

namespace PatternSearch.RabinKarp
{
  public class HashingService : IHashingService
  {
    private readonly int _alphabetSize;
    private readonly int _moduloPrime;

    public HashingService(int alphabetSize, int moduloPrime)
    {
      _alphabetSize = alphabetSize;
      _moduloPrime = moduloPrime;
    }

    public long Hash(long[] t)
    {
      long sum = 0;
      for (var i = 0; i < t.Length; i++)
      {
        sum += (t[i] * (long)Math.Pow(_alphabetSize, t.Length - i - 1) %  _moduloPrime);
      }

      return sum % _moduloPrime;
    }

    public long HashRoll(int patternLength, long hash, long firstElementToRemove, long lastElementToAdd)
    {
      var hashByteToRemove = (firstElementToRemove * (long)Math.Pow(_alphabetSize, patternLength - 1) % _moduloPrime);
      var hashWithoutByteToRemove = hash - hashByteToRemove;
      hashWithoutByteToRemove = hashWithoutByteToRemove < 0
        ? _moduloPrime + hashWithoutByteToRemove
        : hashWithoutByteToRemove;
      return ((hashWithoutByteToRemove * _alphabetSize + lastElementToAdd) % _moduloPrime);
    }
  }
}