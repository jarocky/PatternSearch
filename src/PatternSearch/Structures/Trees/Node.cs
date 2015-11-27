using System;

namespace PatternSearch.Structures.Trees
{
  public class Node<T> : IComparable<Node<T>> where T : IComparable<T>, new()
  {
    public Node<T> Left { get; private set; }
    public Node<T> Right { get; private set; }
    public T Value { get; internal set; }

    public Node(T value)
    {
      Value = value;
    }

    public void AddLeft(T value)
    {
      if (Left != null)
      {
        throw new InvalidOperationException("Left node is occupied");
      }

      Left = new Node<T>(value);
    }

    public void AddRight(T value)
    {

      if (Right != null)
      {
        throw new InvalidOperationException("Left node is occupied");
      }

      Right = new Node<T>(value);
    }

    public int CompareTo(Node<T> other)
    {
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
