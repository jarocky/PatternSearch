using System;

namespace PatternSearch.Comparison
{
  public class Comparer : IComparer
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

      var processingTable = new byte[firstText.Length + 1, 2];
      var result = new ComparisonResult();

      for (var j = 1; j <= secondText.Length + 1; j++)
      {
        var jmodulo = j % 2;
        var jincmodulo = (j + 1) % 2;
        for (var i = 1; i <= firstText.Length + 1; i++)
        {
          if (i < firstText.Length + 1 && j < secondText.Length + 1)
          {
            if (firstText[i - 1] == secondText[j - 1])
            {
              processingTable[i, jmodulo] = (byte)(processingTable[i - 1, jincmodulo] + 1);
            }
            else
            {
              if (processingTable[i - 1, jincmodulo] >= minLength)
              {
                result.Indices.Add(
                  new Tuple<int, int>(i - processingTable[i - 1, jincmodulo] - 1, j - processingTable[i - 1, jincmodulo] - 1),
                  processingTable[i - 1, jincmodulo]);
              }
              processingTable[i, jmodulo] = 0;
            }
          }
          else
          {
            if (processingTable[i - 1, jincmodulo] >= minLength)
            {
              result.Indices.Add(
                new Tuple<int, int>(i - processingTable[i - 1, jincmodulo] - 2, j - processingTable[i - 1, jincmodulo] - 1),
                processingTable[i - 1, jincmodulo]);
            }
          }
        }
      }

      return result;
    }
  }
}