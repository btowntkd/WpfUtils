using System;
using System.Runtime.InteropServices;

namespace WpfUtils
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Int32Parts
    {
        #region Public Fields

        [FieldOffset(0)]
        public Int32 Value;

        [FieldOffset(0)]
        public UInt16 LowWord;

        [FieldOffset(2)]
        public Int16 HighWord;

        #endregion

        #region Constructors

        public Int32Parts(Int32 value)
        {
            //Low and High words must be assigned in constructor;
            //simply zero them out, and the "Value" field will overwrite them
            this.LowWord = 0;
            this.HighWord = 0;
            this.Value = value;
        }

        public Int32Parts(UInt16 low, Int16 high)
        {
            this.Value = 0;
            this.LowWord = low;
            this.HighWord = high;
        }

        #endregion

        #region Public Operators

        public static implicit operator Int32Parts(Int32 value)
        {
            return new Int32Parts(value);
        }

        public static implicit operator Int32(Int32Parts parts)
        {
            return parts.Value;
        }

        #endregion
    }
}
