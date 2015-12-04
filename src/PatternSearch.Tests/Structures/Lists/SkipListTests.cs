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
    public void Insert_FirstWithOneLevel_LeftHeadIsEqualLevel()
    {
      const int level = 0;
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(new Item(0));

      Assert.AreEqual(level, _skipList.LeftHead.Level);
    }

    [Test]
    public void Insert_FirstWithOneLevel_ItemExistsOnTheMaxLevel()
    {
      const int level = 0;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Next.Value);
    }

    [Test]
    public void Insert_FirstWithOneLevel_ItemNotExistsOnPreviousLevel()
    {
      const int level = 0;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.IsNull(_skipList.LeftHead.Next.Down);
    }

    [Test]
    public void Insert_FirstWithTwoLevels_LeftHeadIsEqualLevel()
    {
      const int level = 1;
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(new Item(0));

      Assert.AreEqual(level, _skipList.LeftHead.Level);
    }

    [Test]
    public void Insert_FirstWithTwoLevels_ItemExistsOnTheMaxLevel()
    {
      const int level = 1;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Next.Value);
    }

    [Test]
    public void Insert_FirstWithTwoLevels_ItemExistsOnPreviousLevel()
    {
      const int level = 1;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Next.Down.Value);
    }

    [Test]
    public void Insert_First_ItemExistsOnHeadPreviousLevel()
    {
      const int level = 1;
      var item = new Item(0);
      A.CallTo(() => _randomFake.Next()).Returns(level);

      _skipList.Insert(item);

      Assert.AreEqual(item, _skipList.LeftHead.Down.Next.Value);
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