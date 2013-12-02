using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Numeric
{
    /// <summary>
    /// Interface for data types which support the ability to parse their values from a string.
    /// </summary>
    public interface IParseableDataType
    {
        /// <summary>
        /// Parse and set the current value based on the provided string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        void Parse(string value);
    }
}
