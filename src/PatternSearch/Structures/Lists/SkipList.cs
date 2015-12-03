using System;
using PatternSearch.Common;

namespace PatternSearch.Structures.Lists
{
  public class SkipList<T> where T : IComparable<T>, new()
  {
    private Item<T> _leftHead = new Item<T>(0);
    private readonly IRandomWrapper _random;
    private int _levels = 0;

    internal Item<T> LeftHead { get { return _leftHead; } }

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
      if (_leftHead.Level < _levels)
      {
        leftHeadCurrent = new Item<T>(_levels) { Down = _leftHead };
      }

      var newItem = new Item<T>(value, level);
      var cur = leftHeadCurrent;
      for (var currentLevel = _levels; currentLevel >= 0; currentLevel--)
      {
        while (cur.Next != null || (cur.Next != null && cur.Next.Value.CompareTo(value) < 0))
        {
          if (cur.Next != null && cur.Next.Value.CompareTo(value) == 0)
          {
            return;
          }

          cur = cur.Next;
        }

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

      _leftHead = leftHeadCurrent;
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
      while (cur.Level >= 0 && cur.Next != null && cur.Next.Value.CompareTo(value) <= 0)
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
  }
}