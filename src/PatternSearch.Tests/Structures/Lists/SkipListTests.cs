using System;
using FakeItEasy;
using NUnit.Framework;
using PatternSearch.Common;
using PatternSearch.Structures.Lists;

namespace PatternSearch.Tests.Structures.Lists
{
  [TestFixture]
  public class SkipListTests
  {
    private IRandomWrapper _randomFake;

    private SkipList<Item> _skipList;
      
    [SetUp]
    public void SetUp()
    {
      _randomFake = A.Fake<IRandomWrapper>();
      _skipList = new SkipList<Item>(_randomFake);
    }

    [Test]
    public void Insert_FirstWithProperLevel_LeftHeadIsEqualLevel()
    {
      const int level = 1;
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(new Item(0));

      Assert.AreEqual(level, _skipList.LeftHead.Level);
    }

    [Test]
    public void Insert_FirstWithProperLevel_ItemExistsOnTheMaxLevel()
    {
      const int level = 1;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Next.Value);
    }

    [Test]
    public void Insert_FirstProperLevel_ItemExistsOnTheMaxLevel()
    {
      const int level = 1;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Next.Value);
    }

    private class Item : IComparable<Item>
    {
      public int Value { get; private set; }

      public Item()
      {
        Value = 0;
      }

      public Item(int value)
      {
        Value = value;
      }

      public int CompareTo(Item other)
      {
        if (other == null)
        {
          throw new InvalidOperationException("Object to compare cannot be null");
        }

        if (Value.CompareTo(other.Value) > 0)
        {
          return 1;
        }

        if (Value.CompareTo(other.Value) < 0)
        {
          return -1;
        }

        return 0;
      }
    }
  }
}