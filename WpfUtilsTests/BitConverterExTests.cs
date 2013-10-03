using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace WpfUtilsTest
{
    [TestClass]
    public class BitConverterExTests
    {
        [TestMethod]
        public void GetBytes_FromWords()
        {
            ushort[] input = { 0x0102, 0x0304, 0x0506, 0x0708 };
            byte[] expectedOutput = { 0x02, 0x01, 0x04, 0x03, 0x06, 0x05, 0x08, 0x07 };

            byte[] actualOutput = BitConverterEx.GetBytes(input);

            Assert.AreEqual(BitConverter.ToString(expectedOutput), BitConverter.ToString(actualOutput), "Byte arrays were not equal");
        }
    }
}
