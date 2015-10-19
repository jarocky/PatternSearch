using System;

namespace PatternSearch.Common
{
  public class OperationTimeResult<T> where T : new()
  {
    public OperationTimeResult(T operationResult, TimeSpan elapsed)
    {
      OperationResult = operationResult;
      Elapsed = elapsed;
    }

    public T OperationResult { get; private set; }

    public TimeSpan Elapsed { get; private set; }
  }
}