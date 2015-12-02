using System;
using PatternSearch.Common;

namespace PatternSearch.Structures.Lists
{
  public class SkipList<T> where T : IComparable<T>, new()
  {
    private Item<T> _leftHead = new Item<T>(0);
    private readonly IRandomWrapper _random;
    private int _levels = 0;

    public SkipList(IRandomWrapper random)
    {
      if (random == null)
      {
        throw new ArgumentNullException("random", "Cannot be null");
      }

      _random = random;
    }

    public void Insert(T value)
    {
      if (!Find(value))
      {
        return;
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

      while (_leftHead.Level < _levels)
      {
        _leftHead = new Item<T>(_leftHead.Level + 1) { Down = _leftHead };
      }

      var newItem = new Item<T>(value, level);
      var cur = _leftHead;
      for (var currentLevel = _levels; currentLevel > 0; currentLevel--)
      {
        while (cur.Next != null || cur.Next.Value.CompareTo(value) < 0)
        {
          if (currentLevel == level)
          {
            if (cur.Next == null)
            {
              cur.Next = newItem;
            }
            else
            {
              newItem.Next = cur.Next.Next;
              cur.Next = newItem;
            }
            break;
          }
          cur = cur.Next;
        }
        cur = cur.Down;
      }

      while (cur.Level > 0)
      {
        cur.Next.Down = new Item<T>(cur.Next.Value, cur.Level - 1);
        if (cur.Down.Next != null)
        {
          cur.Next.Down.Next = cur.Down.Next;
        }
        cur.Down.Next = cur.Next.Down;
        cur = cur.Down;
      }
    }

    public bool Find(T value)
    {
      var result = FindItemWithNextEqual(value);
      if (result == null || result.Next == null)
      {
        return false;
      }

      return true;
    }

    private Item<T> FindItemWithNextEqual(T value)
    {
      var cur = _leftHead;
      while (cur.Level >= 0 && (cur.Next != null || cur.Next.Value.CompareTo(value) <= 0))
      {
        if (cur.Next.Value.CompareTo(value) == 0)
        {
          return cur;
        }

        if (cur.Next.Value.CompareTo(value) < 0)
        {
          cur = cur.Down;
        }
      }

      return null;
    }

    public bool Remove(T value)
    {
      var item = FindItemWithNextEqual(value);
      if (item == null || item.Next == null)
      {
        return false;
      }

      for (var i = item.Level; i >= 0; i--)
      {
        item.Next = item.Next.Next;
      }

      return true;
    }
  }
}