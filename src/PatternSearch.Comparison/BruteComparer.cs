using System;

namespace PatternSearch.Comparison
{
  public class BruteComparer : IComparer
  {
    public ComparisonResult Compare(byte[] firstText, byte[] secondText, int minLength)
    {
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
            var index = new Tuple<int, int>(j, j + 1);
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