using System;

namespace PatternSearch.Structures.Lists
{
  internal class Item<T> where T : IComparable<T>,  new()
  {
    public Item<T>[] Next { get; private set; }
    public T Value { get; private set; }

    public Item(T value, int level)
    {
      Value = value;
      Next = new Item<T>[level];
    }

    public Item(int level)
    {
      Value = default(T);
      Next = new Item<T>[level];
    }
  }
}