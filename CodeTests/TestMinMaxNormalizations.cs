using Microsoft.VisualStudio.TestTools.UnitTesting;
using Normalize;
using System;

namespace CodeTests
{
    [TestClass]
    public class TestMinMaxNormalizations
    {
        [TestMethod]
        public void NormalizationsTurnOutToBeHalfWhenThereIsNoDeviationInData()
        {
            double[][] dummyData = new double[4][];
            for (int i = 0; i < dummyData.Length; i++)
            {
                dummyData[i] = new double[1];
                dummyData[i][0] = 25.00;
            }

            Normalizations.MinMaxNormal(dummyData, 0);

            Assert.AreEqual(dummyData[0][0], 0.5);
            Assert.AreEqual(dummyData[1][0], 0.5);
            Assert.AreEqual(dummyData[2][0], 0.5);
            Assert.AreEqual(dummyData[3][0], 0.5);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Input data for Min-Max normalization is null.")]
        public void ExceptionThrownWhenInputDataIsNull()
        {
            Normalizations.MinMaxNormal(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Input `column` cannot be less than zero.")]
        public void ExceptionThrownWhenColumnValueIsLessThanZero()
        {
            double[][] dummyData = new double[4][];
            for (int i = 0; i < dummyData.Length; i++)
            {
                dummyData[i] = new double[1];
                dummyData[i][0] = 25.00;
            }
            Normalizations.MinMaxNormal(dummyData, -1);
        }

        [TestMethod]
        public void NormalizationsTurnOutToBeExpected()
        {
            double[][] dummyData = new double[4][];
            for (int i = 0; i < dummyData.Length; i++)
            {
                dummyData[i] = new double[1];
            }

            dummyData[0][0] = 25.00;
            dummyData[1][0] = 36.00;
            dummyData[2][0] = 40.00;
            dummyData[3][0] = 23.00;

            Normalizations.MinMaxNormal(dummyData, 0);

            Assert.AreEqual(dummyData[0][0], 0.118, 0.001);
            Assert.AreEqual(dummyData[1][0], 0.765, 0.001);
            Assert.AreEqual(dummyData[2][0], 1.0, 0.001);
            Assert.AreEqual(dummyData[3][0], 0.0, 0.001);
        }
    }
}

