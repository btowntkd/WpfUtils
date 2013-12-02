using System;

namespace WpfUtils.Numeric
{
    public class ParseableInt32 : IParseableDataType, IProvideRawData
    {
        private Int32 _value;

        public void Parse(string value)
        {
            Value = Int32.Parse(value);
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public byte[] RawData
        {
            get { return BitConverter.GetBytes(Value); }
        }
    }
}
