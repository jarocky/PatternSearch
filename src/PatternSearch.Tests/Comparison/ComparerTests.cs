using System;
using System.Linq;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Comparison;

namespace PatternSearch.Tests.Comparison
{
  [TestFixture]
  public class ComparerTests
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();

    private readonly Comparer _comparer = new Comparer();

    [Test]
    public void Compare_FirstTextIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _comparer.Compare(null, new byte[1], 1));
    }

    [Test]
    public void Compare_SecondTextIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => _comparer.Compare(new byte[1], null, 1));
    }

    [Test]
    public void Compare_FirstTextIsEmpty_ReturnZeroIndices()
    {
      var result = _comparer.Compare(new byte[0], new byte[1], 1);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [Test]
    public void Compare_SecondTextIsEmpty_ReturnZeroIndices()
    {
      var result = _comparer.Compare(new byte[1], new byte[0], 1);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [Test]
    public void Compare_MinValueIsGreaterThanFirstTextAndSecondTextLengthIsLessThanFirstOne_ReturnZeroIndices()
    {
      var result = _comparer.Compare(new byte[3], new byte[2], 4);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [Test]
    public void Compare_MinValueIsGreaterThanSecondTextAndFirstTextLengthIsLessThanSecondOne_ReturnZeroIndices()
    {
      var result = _comparer.Compare(new byte[2], new byte[3], 4);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [Test]
    public void Compare_MinValueIsZero_ThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _comparer.Compare(new byte[1], new byte[1], 0));
    }

    [Test]
    public void Compare_MinValueIsLesThanZero_ThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => _comparer.Compare(new byte[1], new byte[1], -1));
    }

    [TestCase("A", "B", 1)]
    [TestCase("B", "A", 1)]
    public void Compare_OneDifferentSign_ReturnZeroIdices(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [TestCase("A", "A", 1)]
    [TestCase("B", "B", 1)]
    public void Compare_OneTheSameSign_ReturnOneIndex(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(1, result.Indices.Count);
    }

    [TestCase("A", "A", 1)]
    [TestCase("B", "B", 1)]
    public void Compare_OneTheSameSign_ReturnProperIndex(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(0, result.Indices.Keys.First().Item1);
      Assert.AreEqual(0, result.Indices.Keys.First().Item2);
    }

    [TestCase("AB", "CD", 1)]
    [TestCase("BA", "C", 1)]
    [TestCase("CD", "AB", 1)]
    [TestCase("C", "BA", 1)]
    [TestCase("BC", "BA", 2)]
    [TestCase("CB", "AB", 2)]
    [TestCase("CB", "BA", 2)]
    [TestCase("CBA", "B", 2)]
    [TestCase("CBA", "C", 2)]
    [TestCase("CBA", "A", 2)]
    [TestCase("CBA", "CA", 2)]
    [TestCase("CBA", "BC", 2)]
    [TestCase("CBA", "AB", 2)]
    [TestCase("CBA", "AC", 2)]
    public void Compare_AreNotTheSameSubtexts_ReturnZeroIdices(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(0, result.Indices.Count);
    }

    [TestCase("AB", "A", 1)]
    [TestCase("AB", "B", 1)]
    [TestCase("AB", "BC", 1)]
    [TestCase("AB", "CA", 1)]
    [TestCase("ABC", "AB", 2)]
    [TestCase("ABC", "BC", 2)]
    [TestCase("ABAB", "BA", 2)]
    public void Compare_AreTheSameText_ReturnProperIndicesCount(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(1, result.Indices.Count);
    }
  }
}