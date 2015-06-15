using System;
using System.Runtime.InteropServices;

namespace WpfUtils
{
    /// <summary>
    /// A 32-bit struct which can be used to easily separate the upper and lower 16-bit Word values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct UInt32Parts
    {
        #region Public Fields

        /// <summary>
        /// Get the raw value of the <see cref="UInt32"/>
        /// </summary>
        [FieldOffset(0)]
        public UInt32 Value;

        /// <summary>
        /// Get or Set the Least-Significant Word (16-bits).
        /// </summary>
        [FieldOffset(0)]
        public UInt16 LowWord;

        /// <summary>
        /// Get or Set the Most-Significant Word (16-bits)
        /// </summary>
        [FieldOffset(2)]
        public UInt16 HighWord;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the <see cref="UInt32Parts"/> struct using the given <see cref="UInt32"/> value.
        /// </summary>
        /// <param name="value">The raw <see cref="UInt32"/> value to assign.</param>
        public UInt32Parts(UInt32 value)
        {
            this.LowWord = 0;
            this.HighWord = 0;
            this.Value = value;
        }

        /// <summary>
        /// Create a new instance of the <see cref="UInt32Parts"/> struct using the given low and high 16-bit Words.
        /// </summary>
        /// <param name="low">The lower 16 bits.</param>
        /// <param name="high">The upper 16 bits.</param>
        public UInt32Parts(UInt16 low, UInt16 high)
        {
            this.Value = 0;
            this.LowWord = low;
            this.HighWord = high;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Implicit conversion from <see cref="UInt32"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> from which to convert.</param>
        /// <returns>Returns the equivalent <see cref="UInt32Parts"/> instance.</returns>
        public static implicit operator UInt32Parts(UInt32 value)
        {
            return new UInt32Parts(value);
        }

        /// <summary>
        /// Implicit conversion to <see cref="UInt32"/>.
        /// </summary>
        /// <param name="parts">The <see cref="UInt32Parts"/> instance from which to convert.</param>
        /// <returns>Returns the equivalent <see cref="UInt32"/> instance.</returns>
        public static implicit operator UInt32(UInt32Parts parts)
        {
            return parts.Value;
        }

        #endregion
    }
}
