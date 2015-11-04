using System;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Suffix;

namespace PatternSearch.Tests.Suffix
{
  [TestFixture]
  public class SuffixArrayTests
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();

    [Test]
    public void Constructor_TextIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => new SuffixArray(null));
    }

    [Test]
    public void Initialize_ReturnComparisonsCount()
    {
      const string text = "banana";
      var array = new SuffixArray(_encoder.GetBytes(text));
      var comparisonsCount = array.Initialize();
      Assert.Greater(comparisonsCount, 0);
    }

    [Test]
    public void Find_arrayIsNotInitialized_ThrowInvalidOperationException()
    {
      const string text = "banana";
      var array = new SuffixArray(_encoder.GetBytes(text));

      Assert.Throws<InvalidOperationException>(() => array.Find(_encoder.GetBytes("an")));
    }

    [TestCase("aaa", "c")]
    [TestCase("aba", "c")]
    [TestCase("abb", "c")]
    [TestCase("baa", "c")]
    [TestCase(" aaa", "c")]
    [TestCase("aaa ", "c")]
    [TestCase(" aaa ", "c")]
    [TestCase("banana", "c")]
    [TestCase("ban ana", "c")]
    [TestCase(" ban ana", "c")]
    [TestCase("ban ana ", "c")]
    [TestCase(" ban ana ", "c")]
    [TestCase("aaa", "cc")]
    [TestCase("aba", "cc")]
    [TestCase("abb", "cc")]
    [TestCase("baa", "cc")]
    [TestCase(" aaa", "cc")]
    [TestCase("aaa ", "cc")]
    [TestCase(" aaa ", "cc")]
    [TestCase("banana", "cc")]
    [TestCase("ban ana", "cc")]
    [TestCase(" ban ana", "cc")]
    [TestCase("ban ana ", "cc")]
    [TestCase(" ban ana ", "cc")]
    [TestCase("aaa", "ccc")]
    [TestCase("aba", "ccc")]
    [TestCase("abb", "ccc")]
    [TestCase("baa", "ccc")]
    [TestCase(" aaa", "ccc")]
    [TestCase("aaa ", "ccc")]
    [TestCase(" aaa ", "ccc")]
    [TestCase("banana", "ccc")]
    [TestCase("ban ana", "ccc")]
    [TestCase(" ban ana", "ccc")]
    [TestCase("ban ana ", "ccc")]
    [TestCase(" ban ana ", "ccc")]
    public void Find_PatternDoesNotExistInText_NoResultExists(string text, string pattern)
    {
      var array = new SuffixArray(_encoder.GetBytes(text));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes(pattern));
      Assert.AreEqual(0, results.OccurrencesCount);
    }

    //[TestCase("aaa")]
    //[TestCase("aba")]
    //[TestCase("abb")]
    //[TestCase("baa")]
    //[TestCase(" aaa")]
    //[TestCase("aaa ")]
    [TestCase(" aaa ")]
    //[TestCase("banana")]
    //[TestCase("ban ana")]
    //[TestCase(" ban ana")]
    //[TestCase("ban ana ")]
    //[TestCase(" ban ana ")]
    public void Find_PatternEqualText_OneResultExists(string s)
    {
      var array = new SuffixArray(_encoder.GetBytes(s));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes(s));
      Assert.AreEqual(1, results.OccurrencesCount);
    }

    [TestCase("aaa")]
    [TestCase("aba")]
    [TestCase("abb")]
    [TestCase("baa")]
    [TestCase(" aaa")]
    [TestCase("aaa ")]
    [TestCase(" aaa ")]
    [TestCase("banana")]
    [TestCase("ban ana")]
    [TestCase(" ban ana")]
    [TestCase("ban ana ")]
    [TestCase(" ban ana ")]
    public void Find_PatternEqualText_ReturnIndexZero(string s)
    {
      var array = new SuffixArray(_encoder.GetBytes(s));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes(s));
      Assert.AreEqual(0, results.Indices[0]);
    }

    [Test]
    public void Find_an()
    {
      const string text = "banana";
      var array = new SuffixArray(_encoder.GetBytes(text));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes("an"));
      Assert.AreEqual(2, results.OccurrencesCount);
      Assert.AreEqual(1, results.Indices[0]);
      Assert.AreEqual(3, results.Indices[1]);
    }

    [Test]
    public void Find_sba()
    {
      const string text = "sasbasaasba";
      var array = new SuffixArray(_encoder.GetBytes(text));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes("sba"));
      Assert.AreEqual(2, results.OccurrencesCount);
    }

    [Test]
    public void Find_sa()
    {
      const string text = "sasa";
      var array = new SuffixArray(_encoder.GetBytes(text));
      array.Initialize();
      var results = array.Find(_encoder.GetBytes("sa"));
      Assert.AreEqual(2, results.OccurrencesCount);
    } 
  }
}