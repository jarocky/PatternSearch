using System;

namespace PatternSearch.Suffix
{
  public class SuffixArray : ISuffix
  {
    private readonly Tuple<byte[], int>[] _textSuffixArray;
    private readonly Tuple<byte[], int>[] _suffixArray;
    private bool _initialized;
    private int _buildingComparisonsCount;
    
    public SuffixArray(byte[] text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text", "Cannot be null");
      }

      _textSuffixArray = new Tuple<byte[], int>[text.Length];

      for (var i = 0; i < text.Length; i++)
      {
        var buffer = new byte[text.Length - i];
        Buffer.BlockCopy(text, i, buffer, 0, text.Length - i);
        _textSuffixArray[i] = new Tuple<byte[], int>(buffer, i);
        
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
        var suffix = _textSuffixArray[i];

        var j = 0;
        var comparisonResult = new FindingResult<string>();
        if (_suffixArray[j] != null)
        {
          comparisonResult = CompareBytes(_suffixArray[j].Item1, suffix.Item1);
          comparisons += comparisonResult.ComparisonsCount;
        }
        while (_suffixArray[j] != null && comparisonResult.Result == ComparisonResult.SecondGreaterThanFirst)
        {
          j++;
        }

        for (var k = j; k < i; k++)
        {
          _suffixArray[k] = _suffixArray[k + 1];
        }

        _suffixArray[i] = suffix;
      }

      _buildingComparisonsCount = comparisons;
      _initialized = true;

      return _buildingComparisonsCount;
    }

    public SearchResult Find(byte[] pattern)
    {
      throw new System.NotImplementedException();
    }

    private FindingResult<string> CompareBytes(byte[] text1, byte[] text2)
    {
      var length = Math.Min(text1.Length, text2.Length);
      var comparisons = 0;
      for (var i = 0; i < length; i++)
      {
        comparisons++;
        if (text1[i] > text2[i])
        {
          return new FindingResult<string>
          {
            Result = ComparisonResult.FirstGreaterThanSecond,
            ComparisonsCount = comparisons
          };
        }


        if (text1[i] < text2[i])
        {
          return new FindingResult<string>
          {
            Result = ComparisonResult.SecondGreaterThanFirst,
            ComparisonsCount = comparisons
          };
        }
      }

      return new FindingResult<string>
      {
        Result = ComparisonResult.Equal,
        ComparisonsCount = comparisons
      };
    }
  }
}
