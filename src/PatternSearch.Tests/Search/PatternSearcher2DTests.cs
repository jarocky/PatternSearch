using System;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.RabinKarp;

namespace PatternSearch.Tests.Search
{
  [TestFixture]
  public class PatternSearcher2DTests
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();

    private readonly RabinKarp2DPatternSearcher _searcher = new RabinKarp2DPatternSearcher(new HashingService(256, 13));

    [Test]
    public void Search_PatternIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _searcher.Search(null, new byte[1, 1]));
    }

    [Test]
    public void Search_PatternIsEmpty_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[0, 0], new byte[1, 1]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_PatternIsEmpty_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[0, 0], new byte[1, 1]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [Test]
    public void Search_TextIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _searcher.Search(new byte[0, 0], null));
    }

    [Test]
    public void Search_TextIsEmpty_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[1, 1], new byte[0, 0]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_TextIsEmpty_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[1, 1], new byte[0, 0]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [Test]
    public void Search_PatternVerticalGreaterThanText_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[2, 1], new byte[1, 1]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_PatternHorizontalGreaterThanText_ReturnZeroSearchIterationsCount()
    {
      var result = _searcher.Search(new byte[1, 2], new byte[1, 1]);

      Assert.AreEqual(0, result.ComparisonsCount);
    }

    [Test]
    public void Search_PatternVerticalGreaterThanText_ReturnLackOfIndices()
    {
      var result = _searcher.Search(new byte[2, 1], new byte[1, 1]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [Test]
    public void Search_PatternHorizontalGreaterThanText_ReturnZeroIndices()
    {
      var result = _searcher.Search(new byte[1, 2], new byte[1, 1]);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [TestCase("A", 1, "B", 1)]
    [TestCase("AB", 1, "BA", 1)]
    [TestCase("AB", 2, "BA", 2)]
    [TestCase("ABA", 1, "BAB", 1)]
    [TestCase("ABA", 2, "BAB", 2)]
    [TestCase("ABA", 3, "BAB", 3)]
    [TestCase("AD", 1, "ABDFBDBASSAADABAKDAAA", 3)]
    public void Search_PatternDoesNotExistInText_ReturnZeroIndices(string pattern, int patternVLen, string text, int textVLen)
    {
      var patternArray = _encoder.Get2DArrayBytes(pattern, patternVLen);
      var textArray = _encoder.Get2DArrayBytes(text, textVLen);

      var result = _searcher.Search(patternArray, textArray);

      Assert.AreEqual(0, result.Indices.Length);
    }

    [TestCase("AD", 1, "ABDDBDBASSAADDBAKDAAA", 3)]
    [TestCase("A", 1, "ACCDFFGHDJKIHKJGJCGFCHJVJVFDCLTTYA", 100)]
    [TestCase("A", 1, "ACCDFFGHDJKIHKJGJCGFCHJVJVFDACLTTY", 5)]
    [TestCase("ADDAADDA", 4, "ADDAADDADDAADDAADBASADDAADDAADSDAADD", 4)]
    [TestCase("ADDAADDA", 4, "BADDACADDACDDAAVDDAABDBASADDAFADDALHADSDRAADD", 5)]
    [TestCase("ADDAADDA", 4, "BADDAFCADDAHCDDAAFVDDAAHBDBASFNADDAHRADDADHADSDSRAADDA", 6)]
    [TestCase("ADDAADDA", 4, "BADDADDAHCADDADDADCDDAAFAHCDDAAFAHVD", 9)]
    public void Search_PatternExistsInText_ReturnProperIndicesCount(string pattern, int patternVLen, string text, int textVLen)
    {
      var patternArray = _encoder.Get2DArrayBytes(pattern, patternVLen);
      var textArray = _encoder.Get2DArrayBytes(text, textVLen);

      var result = _searcher.Search(patternArray, textArray);

      Assert.AreEqual(2, result.Indices.Length);
    }

    [TestCase("AD", 1, "ABDDBDBASSAADDBAKDAAA", 3)]
    [TestCase("AHAF", 2, "AHDAFVZAFDAHFCDDAAFGDSSFSFH", 7)]
    public void Search_PatternExistsInText_ReturnProperIndices(string pattern, int patternVLen, string text, int textVLen)
    {
      var patternArray = _encoder.Get2DArrayBytes(pattern, patternVLen);
      var textArray = _encoder.Get2DArrayBytes(text, textVLen);

      var result = _searcher.Search(patternArray, textArray);

      Assert.AreEqual(0, result.Indices[0]);
      Assert.AreEqual(10, result.Indices[1]);
    }
  }
}