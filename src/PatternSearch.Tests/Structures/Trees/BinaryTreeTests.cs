using System;
using NUnit.Framework;
using PatternSearch.Structures.Trees;

namespace PatternSearch.Tests.Structures.Trees
{
  [TestFixture]
  public class BinaryTreeTests
  {
    [Test]
    public void Constructor_NullValue_ThrowArgumentNullException()
    {
      Item value = null;

      Assert.Throws<ArgumentNullException>(() => new BinaryTree<Item>(value));
    }

    [Test]
    public void Insert_NullValue_ThrowArgumentNullException()
    {
      var root = new Item();
      var tree = new BinaryTree<Item>(root);

      Assert.Throws<ArgumentNullException>(() => tree.Insert(null));
    }

    [Test]
    public void Insert_GreaterValueThanRoot_AddRightNode()
    {
      var root = new Item(1);
      var tree = new BinaryTree<Item>(root);
      const int value = 2;

      tree.Insert(new Item(value));

      Assert.AreEqual(value, tree.Root.Right.Value.Value);
    }

    [Test]
    public void Insert_GreaterValueThanRoot_LeftNodeIsNull()
    {
      var root = new Item(1);
      var tree = new BinaryTree<Item>(root);
      const int value = 2;

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void Insert_SmallerValueThanRoot_AddLeftNode()
    {
      var root = new Item(2);
      var tree = new BinaryTree<Item>(root);
      const int value = 1;

      tree.Insert(new Item(value));

      Assert.AreEqual(value, tree.Root.Left.Value.Value);
    }

    [Test]
    public void Insert_SmallerValueThanRoot_RightNodeIsNull()
    {
      var root = new Item(2);
      var tree = new BinaryTree<Item>(root);
      const int value = 1;

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Insert_EqualValueThanRoot_LeftNodeIsNull()
    {
      const int value = 1;
      var root = new Item(value);
      var tree = new BinaryTree<Item>(root);

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void InsertEqualValueThanRoot_RightNodeIsNull()
    {
      const int value = 1;
      var root = new Item(value);
      var tree = new BinaryTree<Item>(root);

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Delete_NullValue_ThrowArgumentNullException()
    {
      var root = new Item();
      var tree = new BinaryTree<Item>(root);

      Assert.Throws<ArgumentNullException>(() => tree.Delete(null));
    }

    [Test]
    public void Search_NullValue_ThrowArgumentNullException()
    {
      var root = new Item();
      var tree = new BinaryTree<Item>(root);

      Assert.Throws<ArgumentNullException>(() => tree.Search(null));
    }

    [Test]
    public void Search_ValueIsEqualRootValue_ReturnTrue()
    {
      const int value = 2;
      var root = new Item(value);
      var tree = new BinaryTree<Item>(root);

      var result = tree.Search(new Item(value));

      Assert.AreEqual(value, result.Result.Value.Value);
    }

    [Test]
    public void Search_ValueIsNotPresentAndThereIsOnlyRoot_ReturnFalse()
    {
      const int value = 2;
      var root = new Item(1);
      var tree = new BinaryTree<Item>(root);

      var result = tree.Search(new Item(value));

      Assert.IsNull(result.Result);
    }

    [Test]
    public void Search_ValueIsNotPresentAndChildren_ReturnFalse()
    {
      const int value = 5;
      var root = new Item(2);
      var tree = new BinaryTree<Item>(root);
      tree.Insert(new Item(1));
      tree.Insert(new Item(3));

      var result = tree.Search(new Item(value));

      Assert.IsNull(result.Result);
    }

    [Test]
    public void Search_ValueIsInLeftChildrenOfRoot_ReturnTrue()
    {
      const int value = 1;
      var root = new Item(2);
      var tree = new BinaryTree<Item>(root);
      tree.Insert(new Item(value));
      tree.Insert(new Item(3));

      var result = tree.Search(new Item(value));

      Assert.AreEqual(value, result.Result.Value.Value);
    }

    [Test]
    public void Search_ValueIsInRightChildrenOfRoot_ReturnTrue()
    {
      const int value = 3;
      var root = new Item(2);
      var tree = new BinaryTree<Item>(root);
      tree.Insert(new Item(1));
      tree.Insert(new Item(value));

      var result = tree.Search(new Item(value));

      Assert.AreEqual(value, result.Result.Value.Value);
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