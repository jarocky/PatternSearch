using System;
using NUnit.Framework;
using PatternSearch.Comparison;

namespace PatternSearch.Tests.Comparison
{
  [TestFixture]
  public class ComparisonResultTests
  {
    [Test]
    public void MaxLength_IsNotAnyElement_ReturnLengthTheElement()
    {
      var result = new ComparisonResult();

      Assert.AreEqual(0, result.MaxLength);
    }

    [Test]
    public void MaxLength_OneElement_ReturnLengthTheElement()
    {
      var result = new ComparisonResult();
      result.Indices.Add(new Tuple<int, int>(0, 0), 1);
      
      Assert.AreEqual(1, result.MaxLength);
    }

    [Test]
    public void MaxLength_TwoElement_ReturnMaxLenthOfElements()
    {
      var result = new ComparisonResult();
      result.Indices.Add(new Tuple<int, int>(0, 0), 1);
      result.Indices.Add(new Tuple<int, int>(0, 1), 2);

      Assert.AreEqual(2, result.MaxLength);
    }

    [Test]
    public void MaxLength_MultipleElement_ReturnMaxLenthOfElements()
    {
      var result = new ComparisonResult();
      result.Indices.Add(new Tuple<int, int>(0, 0), 1);
      result.Indices.Add(new Tuple<int, int>(0, 1), 2);
      result.Indices.Add(new Tuple<int, int>(1, 0), 4);
      result.Indices.Add(new Tuple<int, int>(1, 1), 3);

      Assert.AreEqual(4, result.MaxLength);
    }
  }
}