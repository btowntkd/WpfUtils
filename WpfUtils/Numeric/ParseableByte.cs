using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Numeric
{
    public class ParseableByte : NumericBase<Byte>
    {
        public ParseableByte()
            : base()
        { }

        public ParseableByte(Byte value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = Byte.Parse(value);
        }

        public override byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableByte(Byte rhs)
        {
            return new ParseableByte(rhs);
        }
    }

    public class ParseableSByte : NumericBase<SByte>
    {
        public ParseableSByte()
            : base()
        { }

        public ParseableSByte(SByte value)
            : base(value)
        { }

        public override void Parse(string value)
        {
            Value = SByte.Parse(value);
        }

        public override Byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }

        public static implicit operator ParseableSByte(SByte rhs)
        {
            return new ParseableSByte(rhs);
        }
    }
}
