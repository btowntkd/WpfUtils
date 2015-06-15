using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils
{
    public class ShortCoder
    {
        private readonly string _baseKeyspace;
        private readonly uint _offset;

        /// <summary>
        /// Create a "BaseXX" encoder using the specified set of characters as the digits, and the specified encoding offset for the number '0'.
        /// </summary>
        /// <param name="keyspace">A string with each character representing a 'digit,' in a new base numbering system.</param>
        /// <param name="offset">The 'offset' which will be added to all encoded numbers prior to converting them to a custom keyspace.</param>
        public ShortCoder(string keyspace, uint offset)
        {
            if (string.IsNullOrEmpty(keyspace))
                throw new ArgumentException("keyspace");

            if (keyspace.Distinct().Count() != keyspace.Length)
                throw new ArgumentException("keyspace must not contain duplicate characters");

            _baseKeyspace = keyspace;
            _offset = offset;
        }

        public string Encode(long number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number", "number must be positive integer");

            var resultBuilder = new StringBuilder();
            var remainingNumber = number + _offset;
            var baseLength = _baseKeyspace.Length;

            var i = 0;
            do
            {
                i++;
                var currentDigit = remainingNumber % baseLength;
                resultBuilder.Insert(0, _baseKeyspace[(int)currentDigit]);
                remainingNumber = remainingNumber / baseLength;
            } 
            while (remainingNumber != 0);

            return resultBuilder.ToString();
        }

        public long Decode(string shortcode)
        {
            long result = 0;
            var baseLength = _baseKeyspace.Length;
            for (int x = 0; x < shortcode.Length; x++)
            {
                var currentChar = shortcode[x];
                var currentDigit = _baseKeyspace.IndexOf(currentChar);

                result += currentDigit * (long)Math.Pow(baseLength, shortcode.Length - (x + 1));
            }

            return result - _offset;
        }
    }
}
