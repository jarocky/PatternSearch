using NUnit.Framework;

namespace PatternSearch.Tests.Search
{
  [TestFixture]
  public class SearchResultTests
  {
    [Test]
    public void OccurrencesCount_ReturnIndicesCount()
    {
      const int indicesCount = 2;

      var searchResult = new SearchResult
      {
        ComparisonsCount = 0,
        Indices = new int[indicesCount]
      };

      Assert.AreEqual(indicesCount, searchResult.OccurrencesCount);
    }
  }
}