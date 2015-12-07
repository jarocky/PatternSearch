using System;
using System.Runtime.CompilerServices;
using PatternSearch.Common;

namespace PatternSearch.Structures.Lists
{
  public class SkipList<T> where T : IComparable<T>, new()
  {
    private Item<T> _leftHead = new Item<T>(0);
    private readonly IRandomWrapper _random;
    private int _levels = 0;

    internal Item<T> LeftHead { get { return _leftHead; } }

    public SkipList() : this(new RandomWrapper())
    {
    } 

    public SkipList(IRandomWrapper random)
    {
      if (random == null)
      {
        throw new ArgumentNullException("random", "Cannot be null");
      }

      _random = random;
    }

    public int Insert(T value)
    {
      var comparisonsCount = 0;
      if (_levels == 0 && _leftHead.Next == null)
      {
        _leftHead.Next = new Item<T>(value, 0);
        return comparisonsCount;
      }

      var level = 0;
      for (int r = _random.Next(); (r & 1) == 1; r >>= 1)
      {
        level++;
        if (level > _levels)
        {
          _levels++;
          break;
        }
      }

      var leftHeadCurrent = _leftHead;
      var newItem = new Item<T>(value, level);
      if (_leftHead.Level < _levels)
      {
        leftHeadCurrent = new Item<T>(_levels) { Down = _leftHead };
      }

      var cur = leftHeadCurrent;
      for (var currentLevel = _levels; currentLevel >= 0; currentLevel--)
      {
        comparisonsCount++;
        while (cur.Next != null && cur.Next.Value.CompareTo(value) <= 0)
        {
          comparisonsCount++;
          if (cur.Next.Value.CompareTo(value) == 0)
          {
            return comparisonsCount;
          }

          cur = cur.Next;
          comparisonsCount++;
        }

        if (currentLevel == level)
        {
          if (cur.Next != null)
          {
            newItem.Next = cur.Next.Next;
          }
          cur.Next = newItem;
          break;
        }

        cur = cur.Down;
      }

      comparisonsCount++;
      while (cur.Level > 0)
      {
        var levelNewItem = new Item<T>(value, cur.Level - 1);
        cur.Next.Down = levelNewItem;
        cur = cur.Down;
        comparisonsCount++;
        while (cur.Next != null && cur.Next.Value.CompareTo(value) <= 0)
        {
          comparisonsCount++;
          if (cur.Next.Value.CompareTo(value) == 0)
          {
            return comparisonsCount;
          }

          cur = cur.Next;
          comparisonsCount++;
        }

        comparisonsCount++;
        Item<T> curNextNext = null;
        if (cur.Next != null)
        {
          curNextNext = cur.Next;
        }
        cur.Next = levelNewItem;
        levelNewItem.Next = curNextNext;
        
        comparisonsCount++;
      }

      _leftHead = leftHeadCurrent;

      return comparisonsCount;
    }

    public int Remove(T value)
    {
      var result = FindItemWithNextEqual(value);
      var item = result.Item1;
      var comparisonsCount = result.Item2;
      if (item == null || item.Next == null)
      {
        return comparisonsCount;
      }

      comparisonsCount++;
      for (var i = item.Level; i >= 0; i--)
      {
        item.Next = item.Next.Next;
        comparisonsCount++;
      }

      return comparisonsCount;
    }

    public OperationResult<bool> Find(T value)
    {
      var result = FindItemWithNextEqual(value);
      var comparisonsCount = result.Item2;
      if (result.Item1 == null || result.Item1.Next == null)
      {
        return new OperationResult<bool>()
        {
          Result = false,
          ComparisonsCount = comparisonsCount
        };
      }

      return new OperationResult<bool>()
      {
        Result = true,
        ComparisonsCount = comparisonsCount
      };
    }

    private Tuple<Item<T>, int> FindItemWithNextEqual(T value)
    {
      var cur = _leftHead;
      var comparisonsCount = 0;
      while (cur.Level >= 0)
      {
        comparisonsCount++;
        while (cur.Next != null && cur.Next.Value.CompareTo(value) <= 0)
        {
          comparisonsCount++;
          if (cur.Next.Value.CompareTo(value) == 0)
          {
            return new Tuple<Item<T>, int>(cur, comparisonsCount);
          }
          cur = cur.Next;
          comparisonsCount++;
        }

        if (cur.Down == null)
        {
          break;
        }

        cur = cur.Down;
      }

      return new Tuple<Item<T>, int>(null, comparisonsCount);
    }
  }
}