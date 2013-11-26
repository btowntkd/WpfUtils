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
    /// <typeparam name="TDataType">The underlying data type which is able to be parsed from a string.</typeparam>
    public interface IParseableDataType<TDataType>
    {
        /// <summary>
        /// Parse and set the current value based on the provided string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        void Parse(string value);

        /// <summary>
        /// Get or Set the current value of the underlying data.
        /// </summary>
        TDataType Value { get; set; }
    }
}
