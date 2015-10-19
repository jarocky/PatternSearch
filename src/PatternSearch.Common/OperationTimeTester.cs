using System;
using System.Diagnostics;

namespace PatternSearch.Common
{
  public class OperationTimeTester
  {
    public OperationTimeResult<TR> Test<TP, TR>(Func<TP, TR> operation, TP p) where TR : new()
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      var result = operation(p);
      sw.Stop();

      return new OperationTimeResult<TR>(result, sw.Elapsed);
    }

    public OperationTimeResult<TR> Test<TP1, TP2, TR>(Func<TP1, TP2, TR> operation, TP1 p1, TP2 p2) where TR : new()
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      var result = operation(p1, p2);
      sw.Stop();

      return new OperationTimeResult<TR>(result, sw.Elapsed);
    } 
  }
}