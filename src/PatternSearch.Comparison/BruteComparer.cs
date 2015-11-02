using System;

namespace PatternSearch.Comparison
{
  public class BruteComparer : IComparer
  {
    public ComparisonResult Compare(byte[] firstText, byte[] secondText, int minLength)
    {
      if (firstText == null)
      {
        throw new ArgumentNullException("firstText", "Cannot be null");
      }

      if (secondText == null)
      {
        throw new ArgumentNullException("secondText", "Cannot be null");
      }

      if (firstText.Length == 0 || secondText.Length == 0)
      {
        return new ComparisonResult();
      }

      if (minLength < 1)
      {
        throw new ArgumentException("Must be greater than zero", "minLength");
      }

      if (firstText.Length < minLength || secondText.Length < minLength)
      {
        return new ComparisonResult();
      }

      var result = new ComparisonResult();
      for (var i = 0; i < firstText.Length; i++)
      {
        var j = 0;
        while (j < secondText.Length && j + i < firstText.Length)
        {
          if (firstText[j + i] == secondText[j])
          {
            var length = 0;
            var index = new Tuple<int, int>(j + i, j);
            while (j < secondText.Length && j + i < firstText.Length && firstText[j + i] == secondText[j])
            {
              length++;
              j++;
            }
            if (length >= minLength)
            {
              result.Indices[index] = length;
            }
          }
          j++;
        }
      }

      for (var i = 1; i < secondText.Length; i++)
      {
        var j = 0;
        while (j < firstText.Length && j + i < secondText.Length)
        {
          if (secondText[j + i] == firstText[j])
          {
            var length = 0;
            var index = new Tuple<int, int>(j, j + i);
            while (j < firstText.Length && j + i < secondText.Length && secondText[j + i] == firstText[j])
            {
              length++;
              j++;
            }
            if (length >= minLength)
            {
              result.Indices[index] = length;
            }
          }
          j++;
        }
      }

      return result;
    }
  }

  
}