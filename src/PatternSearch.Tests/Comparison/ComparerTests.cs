using System;
using System.Linq;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Comparison;

namespace PatternSearch.Tests.Comparison
{
  [TestFixture(typeof(BruteComparer))]
  [TestFixture(typeof(Comparer))]
  public class ComparerTests<T> where T : IComparer, new()
  {
    private readonly ByteStringEncoder _encoder = new ByteStringEncoder();

    private readonly IComparer _comparer = new T();

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
    [TestCase("ABAB", "CBA", 2)]
    [TestCase("ABAB", "BAD", 2)]
    [TestCase("ABAB", "CBAD", 2)]
    public void Compare_AreTheSameText_ReturnOneOccurrence(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(1, result.Indices.Count);
    }

    [TestCase("ABABA", "ABA", 3)]
    [TestCase("ABABA", "KABA", 3)]
    [TestCase("ABABA", "ABAC", 3)]
    [TestCase("ABABA", "KABAC", 3)]
    [TestCase("KABABA", "ABA", 3)]
    [TestCase("ABABAC", "ABA", 3)]
    [TestCase("KABABAC", "ABA", 3)]
    [TestCase("KABABAC", "LABA", 3)]
    [TestCase("KABABAC", "ABAM", 3)]
    [TestCase("KABABAC", "LABAM", 3)]
    [TestCase("ABA", "ABABA", 3)]
    [TestCase("KABA", "ABABA", 3)]
    [TestCase("ABAC", "ABABA", 3)]
    [TestCase("KABAC", "ABABA", 3)]
    [TestCase("ABA", "KABABA", 3)]
    [TestCase("ABA", "ABABAC", 3)]
    [TestCase("ABA", "KABABAC", 3)]
    [TestCase("LABA", "KABABAC", 3)]
    [TestCase("ABAM", "KABABAC", 3)]
    [TestCase("LABAM", "KABABAC", 3)]
    public void Compare_AreTheSameText_ReturnTwoOccurrences(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(2, result.Indices.Count);
    }

    [TestCase("ABCWEWYRABCWGROWEABCW", "KLHDSWOPSASBBHJLASABCWDIPHBSDGJKLSDBD", 4)]
    [TestCase("ABCWEWYYRABCWGROWEABCW", "KLHDSWOPSASBBHJLASABCWYDIPHBSDGJKLSDBD", 4)]
    public void Compare_AreTheSameLongerText_ReturnProperIndicesCount(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(3, result.Indices.Count);
    }

    [TestCase("ABCWEWYRABCWGROWEABCW", "KLHDSWOPSASBBHJLASABCWDIPHBSDGJKLSDBD", 4)]
    [TestCase("ABCWEWYRABCWGROWEABCWY", "KLHDSWOPSASBBHJLASABCWYDIPHBSDGJKLSDBD", 4)]
    public void Compare_AreTheSameLongerText_ReturnProperIndices(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(0, result.Indices.Keys.ElementAt(0).Item1);
      Assert.AreEqual(18, result.Indices.Keys.ElementAt(0).Item2);
      Assert.AreEqual(8, result.Indices.Keys.ElementAt(1).Item1);
      Assert.AreEqual(18, result.Indices.Keys.ElementAt(1).Item2);
      Assert.AreEqual(17, result.Indices.Keys.ElementAt(2).Item1);
      Assert.AreEqual(18, result.Indices.Keys.ElementAt(2).Item2);
    }

    [TestCase("ABCWEWYRABCWGROWEQABCW", "KLHDSWOPSASBBHJLASQABCWDIPHBSDGJKLSDBD", 4)]
    [TestCase("ABCWEWYRABCWGROWEQABCWY", "KLHDSWOPSASBBHJLASABCWYDIPHBSDGJKLSDBD", 4)]
    [TestCase("ABCWEWYRABCWGROWEQABCWYX", "KLHDSWOPSASBBHJLASABCWYDIPHBSDGJKLSDBD", 4)]
    public void Compare_AreTheSameLongerText_ReturnProperLenghts(string firstText, string secondText, int minLength)
    {
      var result = _comparer.Compare(_encoder.GetBytes(firstText), _encoder.GetBytes(secondText), minLength);

      Assert.AreEqual(4, result.Indices.Values.ElementAt(0));
      Assert.AreEqual(4, result.Indices.Values.ElementAt(1));
      Assert.AreEqual(5, result.Indices.Values.ElementAt(2));
    }
  }
}