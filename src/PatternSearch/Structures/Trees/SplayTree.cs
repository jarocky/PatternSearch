using System;

namespace PatternSearch.Structures.Trees
{
  public class SplayTree<T> where T : IComparable<T>
  {
    protected internal Node<T> Root { get; protected set; }

    public int Insert(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      var comparisons = 0;
      if (Root == null)
      {
        Root = new Node<T>(value);
        return comparisons;
      }

      var currentNode = Root;
      while (true)
      {
        comparisons++;
        if (value.CompareTo(currentNode.Value) == 0)
        {
          comparisons += Splay(currentNode);
          break;
        }

        comparisons++;
        if (value.CompareTo(currentNode.Value) > 0)
        {
          comparisons++;
          if (currentNode.Right == null)
          {
            currentNode.Right = new Node<T>(value);
            comparisons += Splay(currentNode.Right);
            break;
          }
          
          currentNode = currentNode.Right;
        }
        else
        {
          comparisons++;
          if (currentNode.Left == null)
          {
            currentNode.Left = new Node<T>(value);
            comparisons += Splay(currentNode.Left);
            break;
          }

          currentNode = currentNode.Left;
        }
      }

      return comparisons;
    }

    public int Remove(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      var result = Find(value);
      var comparisons = result.ComparisonsCount;
      var nodeToDelete = result.Result;
      if (nodeToDelete == null)
      {
        return comparisons;
      }

      comparisons += Splay(nodeToDelete);

      if (nodeToDelete.Right != null)
      {
        comparisons++;
        nodeToDelete.Value = nodeToDelete.Right.Value;

        var nodeRightLeft = nodeToDelete.Right.Left;
        nodeToDelete.Right = nodeToDelete.Right.Right;
        var nodeLeft = nodeToDelete.Left;
        nodeToDelete.Left = nodeRightLeft;
        
        var currentNode = nodeToDelete;
        comparisons++;
        while (currentNode.Left != null)
        {
          currentNode = currentNode.Left;
          comparisons++;
        }
        
        currentNode.Left = nodeLeft;
      }
      else if (nodeToDelete.Left != null)
      {
        comparisons++;
        comparisons++;
        nodeToDelete.Value = nodeToDelete.Left.Value;
        var nodeLeftRight = nodeToDelete.Left.Right;
        nodeToDelete.Left = nodeToDelete.Left.Left;
        nodeToDelete.Right = nodeLeftRight;
      }
      else
      {
        throw new InvalidOperationException("Root cannot be deleted");
      }

      return comparisons;
    }

    public OperationResult<Node<T>> Find(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      return Find(value, Root);
    }

    private OperationResult<Node<T>> Find(T value, Node<T> currentNode)
    {
      var comparisons = 0;
      comparisons++;
      while (true)
      {
        comparisons++;
        if (value.CompareTo(currentNode.Value) == 0)
        {
          comparisons += Splay(currentNode);

          return new OperationResult<Node<T>>
          {
            Result = Root,
            ComparisonsCount = comparisons
          };
        }

        comparisons++;
        if (value.CompareTo(currentNode.Value) > 0)
        {
          comparisons++;
          if (currentNode.Right != null)
          {
            currentNode = currentNode.Right;
            continue;
          }
          break;
        }

        comparisons++;
        if (value.CompareTo(currentNode.Value) < 0)
        {
          comparisons++;
          if (currentNode.Left != null)
          {
            currentNode = currentNode.Left;
            continue;
          }
          break;
        }
      }

      return new OperationResult<Node<T>>
      {
        Result = null,
        ComparisonsCount = comparisons
      };
    }

    private int Splay(Node<T> node)
    {
      var currentNode = node;
      var comparisons = 1;
      while (currentNode.Parent != null)
      {
        var currentParent = currentNode.Parent;
        var currentParentParrent = currentNode.Parent.Parent;
        
        comparisons++;
        if (currentParent.Right == currentNode)
        {
          var currentNodeForCheckLeft = currentNode;
          comparisons++;
          while (currentNodeForCheckLeft.Left != null)
          {
            currentNodeForCheckLeft = currentNodeForCheckLeft.Left;
            comparisons++;
          }

          currentNodeForCheckLeft.Left = currentParent;
          currentParent.Right = null;
        }
        else
        {
          var currentNodeForCheckRight = currentNode;
          comparisons++;
          while (currentNodeForCheckRight.Right != null)
          {
            currentNodeForCheckRight = currentNodeForCheckRight.Right;
            comparisons++;
          }

          currentNodeForCheckRight.Right = currentParent;
          currentParent.Left = null;
        }
        
        comparisons++;
        if (currentParentParrent != null)
        {
          comparisons++;
          if (currentParentParrent.Right == currentParent)
          {
            currentParentParrent.Right = currentNode;
          }
          else
          {
            currentParentParrent.Left = currentNode;
          }
        }
        else
        {
          currentNode.Parent = null;
        }

        comparisons++;
      }

      Root = currentNode;

      return comparisons;
    }
  }
}