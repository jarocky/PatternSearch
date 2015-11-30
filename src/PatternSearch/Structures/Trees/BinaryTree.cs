using System;

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

    public int Insert(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      var comparisons = 0;
      var currentNode = Root;
      while (true)
      {
        comparisons++;
        if (value.CompareTo(currentNode.Value) == 0)
        {
          break;
        }

        comparisons++;
        if (value.CompareTo(currentNode.Value) > 0)
        {
          comparisons++;
          if (currentNode.Right == null)
          {
            currentNode.Right = new Node<T>(value);
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
            break;
          }

          currentNode = currentNode.Left;
        }
      }

      return comparisons;
    }

    public int Delete(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      var result = Search(value);
      var comparisons = result.ComparisonsCount;
      var nodeToDelete = result.Result;
      if (nodeToDelete == null)
      {
        return comparisons;
      }

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
      else if(nodeToDelete.Parent != null)
      {
        comparisons++;
        comparisons++;
        comparisons++;
        comparisons++;
        if (nodeToDelete.Parent.Right == nodeToDelete)
        {
          nodeToDelete.Parent.Right = null;
        }
        else
        {
          nodeToDelete.Parent.Left = null;
        }
      }
      else
      {
        throw new InvalidOperationException("Root cannot be deleted");
      }

      return comparisons;
    }

    public OperationResult<Node<T>> Search(T value)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      return Search(value, Root);
    }

    private OperationResult<Node<T>> Search(T value, Node<T> currentNode)
    {
      var comparisons = 0;
      comparisons++;
      while (true)
      {
        comparisons++;
        if (value.CompareTo(currentNode.Value) == 0)
        {
          return new OperationResult<Node<T>>
          {
            Result = currentNode, 
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
  }
}