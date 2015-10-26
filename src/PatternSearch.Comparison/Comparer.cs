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

      var processingTable = new int[firstText.Length + 1, secondText.Length + 1];
      var result = new ComparisonResult();

      for (var j = 1; j <= processingTable.GetLength(1); j++)
      {
        for (var i = 1; i <= processingTable.GetLength(0); i++)
        {
          if (i < processingTable.GetLength(0) && j < processingTable.GetLength(1))
          {
            if (firstText[i - 1] == secondText[j - 1])
            {
              processingTable[i, j] = processingTable[i - 1, j - 1] + 1;
            }
            else
            {
              if (processingTable[i - 1, j - 1] >= minLength)
              {
                result.Indices.Add(
                  new Tuple<int, int>(i - processingTable[i - 1, j - 1] - 1, j - processingTable[i - 1, j - 1] - 1),
                  processingTable[i - 1, j - 1]);
              }
              processingTable[i, j] = 0;
            }
          }
          else
          {
            if (processingTable[i - 1, j - 1] >= minLength)
            {
              result.Indices.Add(
                new Tuple<int, int>(i - processingTable[i - 1, j - 1] - 1, j - processingTable[i - 1, j - 1] - 1),
                processingTable[i - 1, j - 1]);
            }
          }
        }
      }

      return result;
    }
  }
}