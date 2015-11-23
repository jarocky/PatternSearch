using System;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Hashing;
using PatternSearch.RabinKarp;

namespace PatternSearch.Tests.Search
{
  [TestFixture]
  public class RabinKarpPatternSearcherTests
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();
    
    private readonly RabinKarpPatternSearcher _searcher = new RabinKarpPatternSearcher(new HashingService(256, 13));
    
    [Test]
    public void Search_PatternIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _searcher.Search(null, new byte[1]));
    }

    [Test]
    public void Search_PatternIsEmpty_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[0], new byte[1]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_PatternIsEmpty_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[0], new byte[1]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [Test]
    public void Search_TextIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _searcher.Search(new byte[1], null));
    }

    [Test]
    public void Search_TextIsEmpty_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[1], new byte[0]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_TextIsEmpty_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[1], new byte[0]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [Test]
    public void Search_PatternGreaterThanText_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[2], new byte[1]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_PatternGreaterThanText_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[2], new byte[1]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [TestCase("A", "CCDFFGHDJKIHKJGJCGFCHJVJVFDCLTTY")]
    public void Search_OneCharacterPatternDoesNotExistInText_ReturnZeroSearchIterationsCount(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(text.Length, result.ComparisonsCount);
    }

    [TestCase("A", "B")]
    [TestCase("AB", "BA")]
    [TestCase("ABA", "BAB")]
    [TestCase("AD", "ABDDBDBASSASDABDDDAAA")]
    [TestCase("A", "CCDFFGHDJKIHKJGJCGFCHJVJVFDCLTTY")]
    public void Search_PatternDoesNotExistInText_ReturnZeroIndices(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(0, result.Indices.Length);
    }

    [TestCase("co się i", "co się i")]
    [TestCase("co się i", "t co się i")]
    [TestCase("co się i", "co się i z")]
    [TestCase("co się i", "i co się i z")]
    [TestCase("co się i", " co się i z")]
    [TestCase("co się i", "i co się i ")]
    [TestCase("co się i", " co się i ")]
    [TestCase(" co się i ", " co się i ")]
    [TestCase(" co się i ", "i co się i z")]
    [TestCase(" co się i ", " co się i z")]
    [TestCase(" co się i ", "i co się i ")]
    public void Search_PatternExistsOnceInText_ReturnOneOccurrece(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(1, result.OccurrencesCount);
    }

    [TestCase("AD", "ADABDDBDADBASADADSASDABDDDAAAAD")]
    [TestCase("A", "ACCDFAFGHDJKIHKJGAJCGFCHJVJVFDACLTTYA")]
    [TestCase("ADDAADDA", "ADDAADDADDAADDAADBASADDAADDAADSDAADDDAADADDAADDADDAADDA")]
    [TestCase("BAB", "BABABABCACDFAGHDJKIHKJBABABGACGFCHAJVJVFDCLTTA")]
    [TestCase("ABABA", "CABABABABACBABAACDFAGHDJKIHKJGACGFCHAJVJVFDCLTTABABABA")]
    public void Search_PatternExistsInText_ReturnProperIndicesCount(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(5, result.Indices.Length);
    }

    [TestCase("ABABA", "CABABABABACBABAACDFAGHDJKIHKJGACGFCHAJVJVFDCLTTABABABA")]
    public void Search_PatternExistsInText_ReturnProperIndices(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(1, result.Indices[0]);
      Assert.AreEqual(3, result.Indices[1]);
      Assert.AreEqual(5, result.Indices[2]);
      Assert.AreEqual(47, result.Indices[3]);
      Assert.AreEqual(49, result.Indices[4]);
    }

    [TestCase("ABABA", "ABABA")]
    [TestCase("ABAABA", "ABAABA")]
    public void Search_PatternIsEqualText_ReturnIndices(string pattern, string text)
    {
      var result = _searcher.Search(_encoder.GetBytes(pattern), _encoder.GetBytes(text));

      Assert.AreEqual(1, result.Indices.Length);
    }
  }
}