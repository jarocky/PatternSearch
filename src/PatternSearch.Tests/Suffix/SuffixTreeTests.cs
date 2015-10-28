using System.Linq;
using NUnit.Framework;
using PatternSearch.Suffix;

namespace PatternSearch.Tests.Suffix
{
  [TestFixture]
  public class SuffixTreeTests
  {
    [Test]
    public void TestBasics()
    {
      var text = "banana";
      var t = new SuffixTree(text);
      t.Initialize();
      var results = t.Find("an").ToArray();
      Assert.AreEqual(2, results.Length);
      Assert.AreEqual(1, results[0]);
      Assert.AreEqual(3, results[1]);
    }
  }
}