using System;

namespace PatternSearch.HashingStructures
{
  public class Word
  {
    public Word(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentException("Cannot be empty", "value");
      }

      Value = value;
      Count = 1;
    }

    public string Value { get; private set; }

    public int Count { get; private set; }

    public void Increment()
    {
      Count++;
    }

    public void Decrement()
    {
      if (Count == 0)
      {
        throw new InvalidOperationException("Count must be greater or equal zero");
      }

      Count--;
    }
  }
}