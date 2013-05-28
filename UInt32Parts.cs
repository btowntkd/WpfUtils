using System;
using System.Runtime.InteropServices;

namespace WpfUtils
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UInt32Parts
    {
        #region Public Fields

        [FieldOffset(0)]
        public UInt32 Value;

        [FieldOffset(0)]
        public UInt16 LowWord;

        [FieldOffset(2)]
        public UInt16 HighWord;

        #endregion

        #region Constructors

        public UInt32Parts(UInt32 value)
        {
            this.LowWord = 0;
            this.HighWord = 0;
            this.Value = value;
        }

        public UInt32Parts(UInt16 low, UInt16 high)
        {
            this.Value = 0;
            this.LowWord = low;
            this.HighWord = high;
        }

        #endregion

        #region Public Constructors

        public static implicit operator UInt32Parts(UInt32 value)
        {
            return new UInt32Parts(value);
        }

        public static implicit operator UInt32(UInt32Parts parts)
        {
            return parts.Value;
        }

        #endregion
    }
}
