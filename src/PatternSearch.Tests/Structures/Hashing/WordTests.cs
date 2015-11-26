using System;
using NUnit.Framework;
using PatternSearch.Structures.Hashing;

namespace PatternSearch.Tests.Structures.Hashing
{
  [TestFixture]
  public class WordTests
  {
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Constructor_EmptyWordValue_ThrowArgumentException(string value)
    {
      Assert.Throws<ArgumentException>(() => new Word(value));
    }

    [Test]
    public void Constructor_SetWordValue()
    {
      const string elemName = "elem name";

      var word = new Word(elemName);

      Assert.AreEqual(elemName, word.Value);
    }

    [Test]
    public void Constructor_SetWordConter()
    {
      var word = new Word("a");

      Assert.AreEqual(1, word.Count);
    }

    [Test]
    public void Increment_IncrementCount()
    {
      var word = new Word("a");

      word.Increment();

      Assert.AreEqual(2, word.Count);
    }

    [Test]
    public void Decrement_DecrementCount()
    {
      var word = new Word("a");

      word.Decrement();

      Assert.AreEqual(0, word.Count);
    }

    [Test]
    public void Decrement_CountEqualZero_ThrowInvalidOperationException()
    {
      var word = new Word("a");
      word.Decrement();
      
      Assert.Throws<InvalidOperationException>(word.Decrement);
    }
  }
}