using System;

namespace PatternSearch.Structures.Trees
{
  public class Node<T> : IComparable<Node<T>> where T : IComparable<T>, new()
  {
    private Node<T> _parent; 
    private Node<T> _left; 
    private Node<T> _right; 
    private T _value;

    public Node<T> Parent
    {
      get { return _parent; }
      private set
      {
        if (value == null)
        {
          throw new InvalidOperationException("Parrent node cannot be null");
        }

        _parent = value;
      }
    }

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
      _parent = null;
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
