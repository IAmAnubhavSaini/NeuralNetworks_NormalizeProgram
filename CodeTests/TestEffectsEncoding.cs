using Microsoft.VisualStudio.TestTools.UnitTesting;
using Normalize;
using System;

namespace CodeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSpecialCaseWhenNIs2()
        {
            Assert.AreEqual(Encodings.EffectsEncoding(0, 2), "-1");
            Assert.AreEqual(Encodings.EffectsEncoding(1, 2), "1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Values for index can only be 0 or 1.")]
        public void ExceptionThrownWhenIndexValueIsLessThanZero()
        {
            Encodings.EffectsEncoding(-1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Values for index can only be 0 or 1.")]
        public void ExceptionThrownWhenIndexValueIsGreaterThanOne()
        {
            Encodings.EffectsEncoding(2, 2);
        }

        [TestMethod]
        public void TestEffectsEncodingValidOutputs()
        {
            Assert.AreEqual(Encodings.EffectsEncoding(0, 3), "1, 0");
            Assert.AreEqual(Encodings.EffectsEncoding(1, 3), "0, 1");
            Assert.AreEqual(Encodings.EffectsEncoding(2, 3), "-1, -1");
        }
        
        
    }
}
