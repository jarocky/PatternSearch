using System;

namespace PatternSearch.Structures.Trees
{
  public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
  {
    private Node<T> _left; 
    private Node<T> _right; 
    private T _value;

    public Node<T> Parent { get; internal set; }

    public Node<T> Left
    {
      get { return _left; }
      internal set
      {
        _left = value;

        if (_left != null)
        {
          _left.Parent = this;
        }
      }
    }

    public Node<T> Right
    {
      get { return _right; }
      internal set
      {
        _right = value;

        if (_right != null)
        {
          _right.Parent = this;
        }
      }
    }

    public T Value 
    {
      get { return _value; }
      internal set
      {
        if (value == null)
        {
          throw new InvalidOperationException("Value node cannot be null");
        }

        _value = value;
      }
    }

    public Node(T value)
    {
      Value = value;
      Parent = null;
    }

    public int CompareTo(Node<T> other)
    {
      if (_value.CompareTo(other.Value) > 0)
      {
        return 1;
      }

      if (_value.CompareTo(other.Value) < 0)
      {
        return -1;
      }

      return 0;
    }
  }
} 
