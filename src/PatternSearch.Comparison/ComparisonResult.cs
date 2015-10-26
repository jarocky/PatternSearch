using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternSearch.Comparison
{
  public class ComparisonResult
  {
    private readonly Dictionary<Tuple<int, int>, int> _indices = new Dictionary<Tuple<int, int>, int>();

    public Dictionary<Tuple<int, int>, int> Indices
    {
      get
      {
        return _indices;
      }
    }

    public int MaxLength
    {
      get
      {
        if (!_indices.Any())
        {
          return 0;
        }

        return _indices.Values.Max();
      }
    }
  }
}