using System;
using NUnit.Framework;
using PatternSearch.Structures.Lists;

namespace PatternSearch.Tests.Structures.Lists
{
  [TestFixture]
  public class ItemTests
  {
    [Test]
    public void ConstructorWithOneParameter_CorrectArgument_SetLevel()
    {
      const int level = 1;

      var result = new Item<bool>(level);

      Assert.AreEqual(level, result.Level);
    }

    [Test]
    public void ConstructorWithTwoParameters_ValueIsNull_ThrowArgumentNullException()
    {
      Assert.Throws<ArgumentNullException>(() => new Item<string>(null, 1));
    }

    [Test]
    public void ConstructorWithTwoParameters_ValueIsLessThanZero_ThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => new Item<char>(new char(), -1));
    }

    [Test]
    public void ConstructorWithTwoParameters_CorrectArgument_SetValue()
    {
      const char value = new char();

      var result = new Item<char>(value, 1);

      Assert.AreEqual(value, result.Value);
    }

    [Test]
    public void ConstructorWithTwoParameters_CorrectArgument_SetLevel()
    {
      const int level = 1;

      var result = new Item<char>(new char(), level);

      Assert.AreEqual(level, result.Level);
    }

    [Test]
    public void Next_SetNext()
    {
      var item = new Item<char>(1);
      var root = new Item<char>(0);

      root.Next = item;

      Assert.AreEqual(item, root.Next);
    }

    [Test]
    public void Down_SetDown()
    {
      var item = new Item<char>(1);
      var root = new Item<char>(0);

      root.Down = item;

      Assert.AreEqual(item, root.Down);
    }
  }
}