using System;
using System.Dynamic;

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
            currentNode.AddRight(value);
            break;
          }
          
          currentNode = currentNode.Right;
        }
        else
        {
          comparisons++;
          if (currentNode.Left == null)
          {
            currentNode.AddLeft(value);
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

      //todo imoplementation
      //if (nodeToDelete.Right != null)
      //{
      //  nodeToDelete.Value = nodeToDelete.Right.Value;

      //}


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