using System;
using System.Security.Cryptography.X509Certificates;

namespace PatternSearch.Structures.Trees
{
  public class BinaryTree<T> where T : IComparable<T>, new()
  {
    protected internal Node<T> Root { get; protected set; }

    public BinaryTree(T rootValue)
    {
      if (rootValue == null)
      {
        throw new ArgumentNullException("rootValue", "Cannot be null");
      }

      Root = new Node<T>(rootValue);
    }

    public void Insert(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      var currentNode = Root;
      while (true)
      {
        if (value.CompareTo(currentNode.Value) == 0)
        {
          break;
        }

        if (value.CompareTo(currentNode.Value) > 0)
        {
          if (currentNode.Right == null)
          {
            currentNode.AddRight(value);
            break;
          }
          
          currentNode = currentNode.Right;
        }
        else
        {
          if (currentNode.Left == null)
          {
            currentNode.AddLeft(value);
            break;
          }

          currentNode = currentNode.Left;
        }
      }
    }

    public bool Search(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      return Search(value, Root);
    }

    private bool Search(T value, Node<T> currentNode)
    {
      if (value.CompareTo(currentNode.Value) == 0)
      {
        return true;
      }

      if (value.CompareTo(currentNode.Value) > 0)
      {
        if (currentNode.Right != null)
        {
          Search(value, currentNode.Right);
        }
      }

      if (value.CompareTo(currentNode.Value) < 0)
      {
        if (currentNode.Left != null)
        {
          Search(value, currentNode.Left);
        }
      }

      return false;
    }
  }
}