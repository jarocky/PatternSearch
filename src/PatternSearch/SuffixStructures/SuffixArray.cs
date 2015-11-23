using System;
using System.Collections.Generic;

namespace PatternSearch.SuffixStructures
{
  public class SuffixArray : ISuffix
  {
    private readonly byte[][] _textSuffixArray;
    private readonly Tuple<byte[], int>[] _suffixArray;
    private bool _initialized;
    private int _buildingComparisonsCount;

    public SuffixArray(byte[] text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      _textSuffixArray = new byte[text.Length][];

      for (var i = 0; i < text.Length; i++)
      {
        var buffer = new byte[text.Length - i];
        Buffer.BlockCopy(text, i, buffer, 0, text.Length - i);
        _textSuffixArray[i] = buffer;
      }

      _suffixArray = new Tuple<byte[], int>[text.Length];
    }

    public int Initialize()
    {
      if (_initialized)
      {
        return _buildingComparisonsCount;
      }

      var comparisons = 0;
      for (var i = 0; i < _textSuffixArray.Length; i++)
      {
        var suffix = new Tuple<byte[], int>(_textSuffixArray[i], i);

        var j = 0;
        while (_suffixArray[j] != null)
        {
          var comparisonResult = CompareBytes(_suffixArray[j].Item1, suffix.Item1);
          comparisons += comparisonResult.ComparisonsCount;
          if (comparisonResult.Result != ComparisonResult.SecondGreaterThanFirst)
          {
            break;
          }
          j++;
        }

        for (var k = i; k > j; k--)
        {
          _suffixArray[k] = _suffixArray[k - 1];
        }

        _suffixArray[j] = suffix;
      }

      _buildingComparisonsCount = comparisons;
      _initialized = true;

      return _buildingComparisonsCount;
    }

    public SearchResult Find(byte[] pattern)
    {
      if (!_initialized)
      {
        throw new InvalidOperationException("Array is not initialized. Use Initialize method before finding.");
      }

      var comparisons = 0;
      var found = false;
      var i = 0;
      var j = _suffixArray.Length - 1;
      while (i <= j)
      {
        var index = (i + j) / 2;
        var comparisonResult = CompareBytes(_suffixArray[index].Item1, pattern);
        comparisons += comparisonResult.ComparisonsCount;

        if (comparisonResult.Result == ComparisonResult.Equal)
        {
          i = index;
          j = index;
          found = true;
          break;
        }

        if (comparisonResult.Result == ComparisonResult.SecondGreaterThanFirst)
        {
          if (i == index)
          {
            i++;
          }
          else
          {
            i = index;
          }
        }
        else
        {
          if (i == index)
          {
            j--;
          }
          else
          {
            j = index;
          }
        }
      }

      if (!found)
      {
        return new SearchResult
        {
          Indices = new int[0],
          ComparisonsCount = comparisons
        };
      }

      var indixList = new List<int> {_suffixArray[i].Item2};

      i--;
      while (i > 0)
      {
        var comparisonResult = CompareBytes(_suffixArray[i].Item1, pattern);
        comparisons += comparisonResult.ComparisonsCount;
        if (comparisonResult.Result == ComparisonResult.Equal)
        {
          indixList.Add(_suffixArray[i].Item2);
        }
        else
        {
          break;
        }
        i--;
      }

      j++;
      while (j < _textSuffixArray.Length)
      {
        var comparisonResult = CompareBytes(_suffixArray[j].Item1, pattern);
        comparisons += comparisonResult.ComparisonsCount;
        if (comparisonResult.Result == ComparisonResult.Equal)
        {
          indixList.Add(_suffixArray[j].Item2);
        }
        else
        {
          break;
        }
        j++;
      }

      return new SearchResult
      {
        Indices = indixList.ToArray(),
        ComparisonsCount = comparisons
      };
    }

    private FindingResult<string> CompareBytes(byte[] text, byte[] pattern)
    {
      var length = Math.Min(text.Length, pattern.Length);
      var comparisons = 0;
      for (var i = 0; i < length; i++)
      {
        comparisons++;
        if (text[i] > pattern[i])
        {
          return new FindingResult<string>
          {
            Result = ComparisonResult.FirstGreaterThanSecond,
            ComparisonsCount = comparisons
          };
        }


        if (text[i] < pattern[i])
        {
          return new FindingResult<string>
          {
            Result = ComparisonResult.SecondGreaterThanFirst,
            ComparisonsCount = comparisons
          };
        }
      }

      if (pattern.Length > text.Length)
      {
        return new FindingResult<string>
        {
          Result = ComparisonResult.SecondGreaterThanFirst,
          ComparisonsCount = comparisons
        };
      }

      return new FindingResult<string>
      {
        Result = ComparisonResult.Equal,
        ComparisonsCount = comparisons
      };
    }
  }
}
