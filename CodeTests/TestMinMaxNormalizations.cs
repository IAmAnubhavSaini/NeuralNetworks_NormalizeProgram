using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Normalize;

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
        [ExpectedException(typeof(ArgumentNullException), "input data for Min-Max normalization is null.")]
        public void ErrorThrownWhenInputDataIsNull()
        {
            Normalizations.MinMaxNormal(null, 0);
        }
    }
}

