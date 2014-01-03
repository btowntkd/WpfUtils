using System;

namespace WpfUtils.Numeric
{
    public class ParseableInt64 : NumericBase<Int64>
    {
        public ParseableInt64()
            : base()
        { }

        public ParseableInt64(Int64 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = Int64.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableInt64(Int64 rhs)
        {
            return new ParseableInt64(rhs);
        }
    }

    public class ParseableUInt64 : NumericBase<UInt64>
    {
        public ParseableUInt64()
            : base()
        { }

        public ParseableUInt64(UInt64 value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = UInt64.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableUInt64(UInt64 rhs)
        {
            return new ParseableUInt64(rhs);
        }
    }
}
