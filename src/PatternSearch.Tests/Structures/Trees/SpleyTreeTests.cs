using System;
using NUnit.Framework;
using PatternSearch.Structures.Trees;

namespace PatternSearch.Tests.Structures.Trees
{
  [TestFixture]
  public class SplayTreeTests
  {
    [Test]
    public void Insert_NullValue_ThrowArgumentNullException()
    {
      var tree = new SplayTree<Item>();

      Assert.Throws<ArgumentNullException>(() => tree.Insert(null));
    }

    [Test]
    public void Insert_Root_AddRoot()
    {
      var root = 1;
      var tree = new SplayTree<Item>();

      tree.Insert(new Item(root));

      Assert.AreEqual(root, tree.Root.Value.Value);
    }

    [Test]
    public void Insert_Root_RootLeftIsNull()
    {
      var root = 1;
      var tree = new SplayTree<Item>();

      tree.Insert(new Item(root));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void Insert_Root_RootRightIsNull()
    {
      var root = 1;
      var tree = new SplayTree<Item>();

      tree.Insert(new Item(root));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Insert_GreaterValueThanRoot_GreaterValueIsRoot()
    {
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(1));
      const int value = 2;

      tree.Insert(new Item(value));

      Assert.AreEqual(value, tree.Root.Value.Value);
    }

    [Test]
    public void Insert_GreaterValueThanRoot_RightNodeIsNull()
    {
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(1));
      const int value = 2;

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Insert_SmallerValueThanRoot_SmallerValueIsRoot()
    {
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(2));
      const int value = 1;

      tree.Insert(new Item(value));

      Assert.AreEqual(value, tree.Root.Value.Value);
    }

    [Test]
    public void Insert_SmallerValueThanRoot_LeftNodeIsNull()
    {
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(2));
      const int value = 1;

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void Insert_EqualValueThanRoot_LeftNodeIsNull()
    {
      const int value = 1;
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(value));

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void InsertEqualValueThanRoot_RightNodeIsNull()
    {
      const int value = 1;
      var tree = new SplayTree<Item>();
      tree.Insert(new Item(value));

      tree.Insert(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_NullValue_ThrowArgumentNullException()
    {
      var tree = new SplayTree<Item>();

      Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
    }

    [Test]
    public void Remove_RootValue_ThrowInvalidOperationException()
    {
      var root = new Item();
      var tree = new SplayTree<Item>();
      tree.Insert(root);

      Assert.Throws<InvalidOperationException>(() => tree.Remove(root));
    }

    [Test]
    public void Remove_OnlyRight_RootExists()
    {
      const int valueRight = 3;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(valueRight));

      tree.Remove(new Item(valueRight));

      Assert.AreEqual(root, tree.Root.Value);
    }

    [Test]
    public void Remove_OnlyRight_RootLeftIsNull()
    {
      const int value = 3;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value));

      tree.Remove(new Item(value));

      Assert.IsNull(tree.Root.Left);
    }

    [Test]
    public void Remove_OnlyRight_RootRightIsNull()
    {
      const int right = 3;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(right));

      tree.Remove(new Item(right));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_OnlyLeft_RootExists()
    {
      const int valueLeft = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(valueLeft));

      tree.Remove(new Item(valueLeft));

      Assert.AreEqual(root, tree.Root.Value);
    }

    [Test]
    public void Remove_OnlyLeft_RootRightIsNull()
    {
      const int value = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value));

      tree.Remove(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_OnlyLeft_RootLeftIsNull()
    {
      const int value = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value));

      tree.Remove(new Item(value));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_OnlyLeftWithOnlyLeftChild_RootRightIsNull()
    {
      const int value1 = 3;
      const int value2 = 4;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value2));

      tree.Remove(new Item(value1));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_OnlyLeftWithOnlyLeftChild_RootLeftLeftIsNull()
    {
      const int value1 = 3;
      const int value2 = 4;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value2));

      tree.Remove(new Item(value1));

      Assert.IsNull(tree.Root.Left.Left);
    }

    [Test]
    public void Remove_AddOnlyRights_RootRightIsNull()
    {
      const int value1 = 3;
      const int value2 = 4;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value2));

      tree.Remove(new Item(value1));

      Assert.IsNull(tree.Root.Right);
    }

    [Test]
    public void Remove_AddOnlyRights_RootLeftRightIsNull()
    {
      const int value1 = 3;
      const int value2 = 4;
      const int value3 = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value2));
      tree.Insert(new Item(value3));

      tree.Remove(new Item(value1));

      Assert.IsNull(tree.Root.Left.Right);
    }

    [Test]
    public void Remove_Root_RootHasProperValue()
    {
      const int value1 = 5;
      const int value2 = 4;
      const int value3 = 6;
      const int value4 = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value3));
      tree.Insert(new Item(value4));
      tree.Insert(new Item(value2));

      tree.Remove(root);

      Assert.AreEqual(value2, tree.Root.Value.Value);
    }

    [Test]
    public void Remove_Root_RootLeftHasProperValue()
    {
      const int value1 = 5;
      const int value2 = 4;
      const int value3 = 6;
      const int value4 = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value3));
      tree.Insert(new Item(value4));
      tree.Insert(new Item(value2));

      tree.Remove(root);

      Assert.AreEqual(value4, tree.Root.Left.Value.Value);
    }

    [Test]
    public void Remove_Root_RootRightHasProperValue()
    {
      const int value1 = 5;
      const int value2 = 4;
      const int value3 = 6;
      const int value4 = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value1));
      tree.Insert(new Item(value3));
      tree.Insert(new Item(value4));
      tree.Insert(new Item(value2));

      tree.Remove(root);

      Assert.AreEqual(value2, tree.Root.Value.Value);
    }

    [Test]
    public void Remove_Root_RootRightLeftIsNull()
    {
      const int valueRight = 5;
      const int valueRightLeft = 4;
      const int valueRightRight = 6;
      const int valueLeft = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(valueRight));
      tree.Insert(new Item(valueRightRight));
      tree.Insert(new Item(valueLeft));
      tree.Insert(new Item(valueRightLeft));

      tree.Remove(root);

      Assert.IsNull(tree.Root.Right.Left);
    }

    [Test]
    public void Remove_Root_RootLeftRightIsNull()
    {
      const int valueRight = 5;
      const int valueRightLeft = 4;
      const int valueRightRight = 6;
      const int valueLeft = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(valueRight));
      tree.Insert(new Item(valueRightRight));
      tree.Insert(new Item(valueLeft));
      tree.Insert(new Item(valueRightLeft));

      tree.Remove(root);

      Assert.IsNull(tree.Root.Left.Right);
    }

    [Test]
    public void Find_NullValue_ThrowArgumentNullException()
    {
      var root = new Item();
      var tree = new SplayTree<Item>();
      tree.Insert(root);

      Assert.Throws<ArgumentNullException>(() => tree.Find(null));
    }

    [Test]
    public void Find_ValueIsEqualRootValue_ReturnTrue()
    {
      const int value = 2;
      var root = new Item(value);
      var tree = new SplayTree<Item>();
      tree.Insert(root);

      var result = tree.Find(new Item(value));

      Assert.AreEqual(value, result.Result.Value.Value);
    }

    [Test]
    public void Find_ValueIsNotPresentAndThereIsOnlyRoot_ReturnFalse()
    {
      const int value = 2;
      var root = new Item(1);
      var tree = new SplayTree<Item>();
      tree.Insert(root);

      var result = tree.Find(new Item(value));

      Assert.IsNull(result.Result);
    }

    [Test]
    public void Find_ValueIsNotWithChildren_ReturnFalse()
    {
      const int value = 5;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(1));
      tree.Insert(new Item(3));

      var result = tree.Find(new Item(value));

      Assert.IsNull(result.Result);
    }

    [Test]
    public void Find_ValueIsPresentwithChildren_ReturnValue()
    {
      const int value = 1;
      var root = new Item(2);
      var tree = new SplayTree<Item>();
      tree.Insert(root);
      tree.Insert(new Item(value));
      tree.Insert(new Item(3));

      var result = tree.Find(new Item(value));

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