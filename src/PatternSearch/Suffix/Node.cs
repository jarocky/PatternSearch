using System.Collections.Generic;

namespace PatternSearch.Suffix
{
  internal class Node
  {
    public int Index = -1;
    public Dictionary<int, Node> Children = new Dictionary<int, Node>();
  }
}