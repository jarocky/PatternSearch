using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PatternSearch.Hashing;

namespace PatternSearch.Structures.Hashing
{
  public class HashingArray
  {
    private readonly List<Word>[] _array;
    private readonly IHashingService _hashingService;

    public HashingArray(IHashingService hashingService, int length)
    {
      _array = new List<Word>[length];
      _hashingService = hashingService;
    }

    public ReadOnlyCollection<Word> this[int index]
    {
      get { return new ReadOnlyCollection<Word>(_array[index]); }
    }

    public void Add(string s)
    {
      var hash = _hashingService.Hash(s);
      var element = _array[hash];

      if (element == null)
      {
        _array[hash] = new List<Word> { new Word(s) };
        return;
      }

      var word = element.SingleOrDefault(w => w.Value == s);
      if (word == null)
      {
        element.Add(new Word(s));
        return;
      }

      word.Increment();
    }
  }
}