using System;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.SuffixStructures;

namespace PatternSearch.Tests.SuffixStructures
{
  [TestFixture]
  public class SuffixTreeTests
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();

    [Test]
    public void Constructor_TextIsNull_ThrowArgumentNullException()
    {
     Assert.Throws<ArgumentNullException>(() => new SuffixTree(null));
    }

    [Test]
    public void Initialize_ReturnComparisonsCount()
    {
      const string text = "banana";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      var comparisonsCount = tree.Initialize();
      Assert.Greater(comparisonsCount, 0);
    }
    
    [Test]
    public void Find_TreeIsNotInitialized_ThrowInvalidOperationException()
    {
      const string text = "banana";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      
      Assert.Throws<InvalidOperationException>(() => tree.Find(_encoder.GetBytes("an")));
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
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes(pattern));
      Assert.AreEqual(0, results.OccurrencesCount);
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
    public void Find_PatternEqualText_OneResultExists(string s)
    {
      var tree = new SuffixTree(_encoder.GetBytes(s));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes(s));
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
      var tree = new SuffixTree(_encoder.GetBytes(s));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes(s));
      Assert.AreEqual(0, results.Indices[0]);
    }
    
    [Test]
    public void Find_an()
    {
      const string text = "banana";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes("an"));
      Assert.AreEqual(2, results.OccurrencesCount);
      Assert.AreEqual(3, results.Indices[0]);
      Assert.AreEqual(1, results.Indices[1]);
    }

    [Test]
    public void Find_sba()
    {
      const string text = "sasbasaasba";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes("sba"));
      Assert.AreEqual(2, results.OccurrencesCount);
    }

    [Test]
    public void Find_sa()
    {
      const string text = "sasa";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes("sa"));
      Assert.AreEqual(2, results.OccurrencesCount);
    }
  }
}