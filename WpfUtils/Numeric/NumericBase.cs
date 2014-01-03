using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Numeric
{
    public abstract class NumericBase<TNumeric> : IParseableDataType, IProvideRawData
    {
        private TNumeric _value;

        public NumericBase()
            : this(default(TNumeric))
        { }

        public NumericBase(TNumeric value)
        {
            Value = value;
        }

        public TNumeric Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public abstract void Parse(string value);

        public abstract byte[] RawData { get; }

        public static implicit operator TNumeric(NumericBase<TNumeric> rhs)
        {
            return rhs.Value;
        }
    }
}
