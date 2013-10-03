using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils
{
    /// <summary>
    /// Provides additional BitConverter functionality.
    /// </summary>
    public static class BitConverterEx
    {
        public static byte[] GetBytes(UInt16[] words)
        {
            return words.SelectMany((x) => BitConverter.GetBytes(x)).ToArray();
        }
    }
}
