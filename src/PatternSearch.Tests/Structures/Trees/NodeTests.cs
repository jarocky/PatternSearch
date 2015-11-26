using System;
using NUnit.Framework;
using PatternSearch.Structures.Trees;

namespace PatternSearch.Tests.Structures.Trees
{
  [TestFixture]
  public class NodeTests
  {
    [Test]
    public void Constructor_CorrectValue_SetValue()
    {
      var value = new Item();

      var node = new Node<Item>(value);

      Assert.AreEqual(value, node.Value);
    }

    [Test]
    public void AddLeft_LeftElementIsSet_ThrowInvalidOperationException()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.AddLeft(root);

      Assert.Throws<InvalidOperationException>(() => rootNode.AddLeft(root));
    }

    [Test]
    public void AddLeft_LeftElementIsNull_SetLeftNode()
    {
      var root = new Item();
      var node = new Item();
      var rootNode = new Node<Item>(root);
      
      rootNode.AddLeft(node);

      Assert.AreEqual(node, rootNode.Left.Value);
    }

    [Test]
    public void AddRight_LeftElementIsSet_ThrowInvalidOperationException()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.AddRight(root);

      Assert.Throws<InvalidOperationException>(() => rootNode.AddRight(root));
    }

    [Test]
    public void AddRight_LeftElementIsNull_SetLeftNode()
    {
      var root = new Item();
      var node = new Item();
      var rootNode = new Node<Item>(root);

      rootNode.AddRight(node);

      Assert.AreEqual(node, rootNode.Right.Value);
    }

    [Test]
    public void CompareTo_CompareToGreaterItem()
    {
      var root = new Item(1);
      var node = new Item(2);

      var result = root.CompareTo(node);

      Assert.AreEqual(-1, result);
    }

    [Test]
    public void CompareTo_CompareToSmallerItem()
    {
      var root = new Item(2);
      var node = new Item(1);

      var result = root.CompareTo(node);

      Assert.AreEqual(1, result);
    }

    [Test]
    public void CompareTo_CompareToEqualItem()
    {
      var root = new Item(2);
      var node = new Item(2);

      var result = root.CompareTo(node);

      Assert.AreEqual(0, result);
    }

    private class Item : IComparable<Item>
    {
      private int Value { get; set; }

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