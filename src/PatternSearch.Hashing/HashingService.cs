namespace PatternSearch.Hashing
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
        sum += t[i] * PowMod(_alphabetSize, t.Length - i - 1, _moduloPrime) % _moduloPrime;
      }

      return sum % _moduloPrime;
    }

    public long HashRoll(int patternLength, long hash, long firstElementToRemove, long lastElementToAdd)
    {
      var hashByteToRemove = firstElementToRemove * PowMod(_alphabetSize, patternLength - 1, _moduloPrime) % _moduloPrime;;
      var hashWithoutByteToRemove = hash - hashByteToRemove;
      hashWithoutByteToRemove = hashWithoutByteToRemove < 0
        ? _moduloPrime + hashWithoutByteToRemove
        : hashWithoutByteToRemove;
      return ((hashWithoutByteToRemove * _alphabetSize + lastElementToAdd) % _moduloPrime);
    }

    public long PowMod(long number, long exp, long modulus)
    {
      long result = 1;
      while (exp > 0)
      {
        if ((exp & 1) != 0)
        {
          result = (result * number) % modulus;
        }
        number = (number * number) % modulus;
        exp >>= 1;
      }
      return result;
    }
  }
}