using System;
using System.Text;

namespace PatternSearch.Common
{
  public class ByteStringEncoder
  {
    public byte[] GetBytes(string str)
    {
      return Encoding.Default.GetBytes(str);
    }

    public string GetString(byte oneByte)
    {
      return GetString(new byte[] { oneByte });
    }

    public string GetString(byte[] bytes)
    {
      return Encoding.Default.GetString(bytes);
    }

    public string GetStringTo2DArray(byte[,] bytes)
    {
      var byteArray = new byte[bytes.Length];
      Buffer.BlockCopy(bytes, 0, byteArray, 0, bytes.Length);
      return GetString(byteArray);
    }

    public byte[,] Get2DArrayBytes(string str, int k)
    {
      if (str.Length < k)
      {
        k = str.Length;
      }
      var h = str.Length/k;
      var array = new byte[k, h];
      for (var j = 0; j < array.GetLength(1); j++)
      {
        for (var i = 0; i < array.GetLength(0); i++)
        {
          array[i, j] = Encoding.Default.GetBytes((str[i + j * k]).ToString())[0];
        }
      }

      return array;
    } 
  }
}