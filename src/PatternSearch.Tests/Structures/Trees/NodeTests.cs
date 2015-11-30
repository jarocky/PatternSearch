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
    public void Constructor_CorrectValue_ParentIsNull()
    {
      var value = new Item();

      var node = new Node<Item>(value);

      Assert.IsNull(node.Parent);
    }

    [Test]
    public void AddLeft_LeftElementIsSet_SetNewLeftElement()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.Left = rootNode;
      var node = new Item();

      rootNode.Left = new Node<Item>(node);

      Assert.AreEqual(rootNode.Left.Value, node);
    }

    [Test]
    public void AddLeft_LeftElementIsNull_SetLeftNode()
    {
      var root = new Item();
      var node = new Item();
      var rootNode = new Node<Item>(root);

      rootNode.Left = new Node<Item>(node);

      Assert.AreEqual(node, rootNode.Left.Value);
    }

    [Test]
    public void AddLeft_LeftElementIsSet_SetNewParentElement()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.Left = rootNode;
      var node = new Node<Item>(new Item());

      rootNode.Left = node;

      Assert.AreEqual(rootNode, rootNode.Left.Parent);
    }

    [Test]
    public void AddRight_LeftElementIsSet_SetNewRightElement()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.Right = new Node<Item>(root);
      var node = new Item();

      rootNode.Right = new Node<Item>(node);

      Assert.AreEqual(node, rootNode.Right.Value);
    }

    [Test]
    public void AddRight_LeftElementIsNull_SetLeftNode()
    {
      var root = new Item();
      var node = new Item();
      var rootNode = new Node<Item>(root);

      rootNode.Right = new Node<Item>(node);

      Assert.AreEqual(node, rootNode.Right.Value);
    }

    [Test]
    public void AddLeft_RightElementIsSet_SetNewParentElement()
    {
      var root = new Item();
      var rootNode = new Node<Item>(root);
      rootNode.Right = rootNode;
      var node = new Node<Item>(new Item());

      rootNode.Right = node;

      Assert.AreEqual(rootNode, rootNode.Right.Parent);
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