using System;

namespace WpfUtils.Numeric
{
    public class ParseableInt32 : NumericBase<Int32>
    {
        public ParseableInt32()
            : base()
        { }

        public ParseableInt32(Int32 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = Int32.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableInt32(Int32 rhs)
        {
            return new ParseableInt32(rhs);
        }
    }

    public class ParseableUInt32 : NumericBase<UInt32>
    {
        public ParseableUInt32()
            : base()
        { }

        public ParseableUInt32(UInt32 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = UInt32.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableUInt32(UInt32 rhs)
        {
            return new ParseableUInt32(rhs);
        }
    }
}
