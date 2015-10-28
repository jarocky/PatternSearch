using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  internal class Node
  {
    public int Index = -1;
    public Dictionary<char, Node> Children = new Dictionary<char, Node>();
  }
}