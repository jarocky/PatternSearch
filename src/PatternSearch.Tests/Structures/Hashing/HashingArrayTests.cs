using FakeItEasy;
using NUnit.Framework;
using PatternSearch.Hashing;
using PatternSearch.Structures.Hashing;

namespace PatternSearch.Tests.Structures.Hashing
{
  [TestFixture]
  public class HashingArrayTests
  {
    private IHashingService _hashingServiceFake;

    private HashingArray _array;

    [SetUp]
    public void SetUp()
    {
      _hashingServiceFake = A.Fake<IHashingService>();
      _array = new HashingArray(_hashingServiceFake, 10);
    }

    [Test]
    public void Add_AddString_WordExists()
    {
      const string word = "word";
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(word)).Returns(hash);
      
      _array.Add(word);

      var result = _array[hash];
      Assert.AreEqual(word, result[0].Value);
    }

    [Test]
    public void Add_AddString_OneWordExists()
    {
      const string word = "word";
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(word)).Returns(hash);

      _array.Add(word);

      var result = _array[hash];
      Assert.AreEqual(1, result.Count);
    }

    [Test]
    public void Add_AddString_WordCountEqualsOne()
    {
      const string word = "word";
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(word)).Returns(hash);

      _array.Add(word);

      var result = _array[hash];
      Assert.AreEqual(1, result[0].Count);
    }

    [Test]
    public void Add_AddTwoDifferentStringsWithTheSameHash_TwoWordExists()
    {
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(A<string>._)).Returns(hash);

      _array.Add("word1");
      _array.Add("word2");

      var result = _array[hash];
      Assert.AreEqual(2, result.Count);
    }

    [Test]
    public void Add_AddStringTwice_OneWordExists()
    {
      const string word = "word";
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(word)).Returns(hash);

      _array.Add(word);
      _array.Add(word);

      var result = _array[hash];
      Assert.AreEqual(1, result.Count);
    }

    [Test]
    public void Add_AddStringTwice_WordContEqualTwo()
    {
      const string word = "word";
      const int hash = 2;
      A.CallTo(() => _hashingServiceFake.Hash(word)).Returns(hash);

      _array.Add(word);
      _array.Add(word);

      var result = _array[hash];
      Assert.AreEqual(2, result[0].Count);
    }
  }
}