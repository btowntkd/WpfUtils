using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfUtils;

namespace WpfUtilsTest
{
    [TestClass]
    public class ShortCoderTests
    {
        [TestMethod]
        public void Encode_IdentityTransform_ResultsInSameNumber()
        {
            var coder = new ShortCoder("0123456789", 0);

            var valueToEncode = (Int64)0;
            var encodedResult = coder.Encode(valueToEncode);
            Assert.AreEqual(encodedResult, valueToEncode.ToString());

            valueToEncode = Int64.MaxValue;
            encodedResult = coder.Encode(valueToEncode);
            Assert.AreEqual(encodedResult, valueToEncode.ToString());
        }

        [TestMethod]
        public void Decode_IdentityTransform_ResultsInSameNumber()
        {
            var coder = new ShortCoder("0123456789", 0);

            var valueToDecode = ((Int64)0).ToString();
            var decodedResult = coder.Decode(valueToDecode);
            Assert.AreEqual(decodedResult.ToString(), valueToDecode);

            valueToDecode = Int64.MaxValue.ToString();
            decodedResult = coder.Decode(valueToDecode);
            Assert.AreEqual(decodedResult.ToString(), valueToDecode);
        }

        [TestMethod]
        public void EncodeDecode_RoundTrip()
        {
            var coder = new ShortCoder("abcdefg012345678", 1234);
            var rand = new Random();

            for (int x = 0; x < 100; x++)
            {
                var startValue = rand.Next();
                var encodedValue = coder.Encode(startValue);
                var decodedValue = coder.Decode(encodedValue);

                Assert.AreEqual(startValue, decodedValue, string.Format("Value was {0}, expected {1}", decodedValue, startValue));
            }
        }
    }
}
