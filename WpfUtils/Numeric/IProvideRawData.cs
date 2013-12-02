using System;

namespace WpfUtils.Numeric
{
    /// <summary>
    /// Interface through which a data type can provide raw bytes representing the underlying value.
    /// </summary>
    interface IProvideRawData
    {
        /// <summary>
        /// Get the raw underlying bytes from the object.
        /// </summary>
        Byte[] RawData { get; }
    }
}
