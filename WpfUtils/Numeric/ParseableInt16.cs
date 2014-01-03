using System;

namespace WpfUtils.Numeric
{
    public class ParseableInt16 : NumericBase<Int16>
    {
        public ParseableInt16()
            : base()
        { }

        public ParseableInt16(Int16 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = Int16.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableInt16(Int16 rhs)
        {
            return new ParseableInt16(rhs);
        }
    }

    public class ParseableUInt16 : NumericBase<UInt16>
    {
        public ParseableUInt16()
            : base()
        { }

        public ParseableUInt16(UInt16 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = UInt16.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableUInt16(UInt16 rhs)
        {
            return new ParseableUInt16(rhs);
        }
    }
}
