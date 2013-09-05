using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace WpfUtils.Tests
{
    [TestClass]
    public class UInt32PartsTests
    {
        [TestMethod]
        public void Implicit_ToUInt32_Matches_Value()
        {
            UInt32 expectedValue = 0x12345678;
            UInt32Parts test = new UInt32Parts(expectedValue);

            Assert.AreEqual(expectedValue, (UInt32)test, "Results from implicit UInt32 operator did not match the input value");
        }

        [TestMethod]
        public void Implicit_ToUInt32Parts_Matches_Value()
        {
            UInt32 expectedValue = 0x12345678;
            UInt32Parts test = (UInt32Parts)expectedValue;

            Assert.AreEqual(expectedValue, (UInt32)test, "Results from implicit UInt32 operator did not match the input value");
        }

        [TestMethod]
        public void GetLowPart_Equals_LeastSignificantWord()
        {
            UInt32 expectedValue = 0x12345678;
            UInt32Parts test = new UInt32Parts(expectedValue);

            Assert.AreEqual((UInt16)0x5678, test.LowWord, "Results from LowPart did not match the least-significant 16-bits.");
        }

        [TestMethod]
        public void GetHighPart_Equals_MostSignificantWord()
        {
            UInt32 expectedValue = 0x12345678;
            UInt32Parts test = new UInt32Parts(expectedValue);

            Assert.AreEqual((UInt16)0x1234, test.HighWord, "Results from HighPart did not match the least-significant 16-bits.");
        }
    }
}
