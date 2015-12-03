using System;

namespace PatternSearch.Structures.Lists
{
  internal class Item<T> where T : IComparable<T>
  {
    public Item<T> Next { get; internal set; }
    public Item<T> Down { get; internal set; }
    public T Value { get; private set; }
    public int Level { get; private set; }

    public Item(int level)
    {
      Level = level;
    }

    public Item(T value, int level)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value", "Cannot be null");
      }

      if (level < 0)
      {
        throw new ArgumentException("Cannot be less than null", "level");
      }

      Value = value;
      Level = level;
    }
  }
}