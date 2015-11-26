using System;
using System.Collections.Generic;

namespace PatternSearch.Structures.Lists
{
  public class SkipList<T> where T : IComparable<T>, new()
  {
    private readonly Item<T> _head = new Item<T>(33); // The max. number of levels is 33
    private readonly Random _rand = new Random();
    private int _levels = 1;

    public void Insert(T value)
    {
      // Determine the level of the new node. Generate a random number R. The number of
      // 1-bits before we encounter the first 0-bit is the level of the node. Since R is
      // 32-bit, the level can be at most 32.
      var level = 0;
      for (int r = _rand.Next(); (r & 1) == 1; r >>= 1)
      {
        level++;
        if (level == _levels)
        {
          _levels++; 
          break;
        }
      }

      var newItem = new Item<T>(value, level + 1);
      var cur = _head;
      for (var i = _levels - 1; i >= 0; i--)
      {
        for (; cur.Next[i] != null; cur = cur.Next[i])
        {
          if (cur.Next[i].Value.CompareTo(value) > 0)
          {
            break;
          }
        }

        if (i <= level)
        {
          newItem.Next[i] = cur.Next[i]; 
          cur.Next[i] = newItem;
        }
      }
    }

    public bool Contains(T value)
    {
      Item<T> cur = _head;
      for (int i = _levels - 1; i >= 0; i--)
      {
        for (; cur.Next[i] != null; cur = cur.Next[i])
        {
          if (cur.Next[i].Value.CompareTo(value) > 0)
          {
            break;
          }
          if (cur.Next[i].Value.CompareTo(value) == 0)
          {
            return true;
          }
        }
      }
      return false;
    }

    public bool Remove(T value)
    {
      var cur = _head;

      var found = false;
      for (int i = _levels - 1; i >= 0; i--)
      {
        for (; cur.Next[i] != null; cur = cur.Next[i])
        {
          if (cur.Next[i].Value.CompareTo(value) == 0)
          {
            found = true;
            cur.Next[i] = cur.Next[i].Next[i];
            break;
          }

          if (cur.Next[i].Value.CompareTo(value) > 0) break;
        }
      }

      return found;
    }

    public IEnumerable<T> Enumerate()
    {
      var cur = _head.Next[0];
      while (cur != null)
      {
        yield return cur.Value;
        cur = cur.Next[0];
      }
    }
  }
}