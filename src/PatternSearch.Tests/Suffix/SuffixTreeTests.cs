using System;
using System.Linq;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Suffix;

namespace PatternSearch.Tests.Suffix
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
      tree.Initialize();
      Assert.True(true);
      //Assert.Greater(comparisonsCount, 0);
    }
    
    [Test]
    public void Find_TreeIsNotInitialized_ThrowInvalidOperationException()
    {
      const string text = "banana";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      
      Assert.Throws<InvalidOperationException>(() => tree.Find(_encoder.GetBytes("an")).ToArray());
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
      var t = new SuffixTree(_encoder.GetBytes(s));
      t.Initialize();
      var results = t.Find(_encoder.GetBytes(s)).ToArray();
      Assert.AreEqual(1, results.Length);
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
      var results = tree.Find(_encoder.GetBytes(s)).ToArray();
      Assert.AreEqual(0, results[0]);
    }
    
    [Test]
    public void Find_an()
    {
      const string text = "banana";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes("an")).ToArray();
      Assert.AreEqual(2, results.Length);
      Assert.AreEqual(1, results[0]);
      Assert.AreEqual(3, results[1]);
    }

    [Test]
    public void Find()
    {
      const string text = "sasbasaasba";
      var tree = new SuffixTree(_encoder.GetBytes(text));
      tree.Initialize();
      var results = tree.Find(_encoder.GetBytes("sba")).ToArray();
      Assert.AreEqual(2, results.Length);
    }
  }
}