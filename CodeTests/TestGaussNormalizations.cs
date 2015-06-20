using Microsoft.VisualStudio.TestTools.UnitTesting;
using Normalize;
using System;

namespace CodeTests
{
    [TestClass]
    public class TestGaussNormalizations
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "All the values entered are same, is it even a good input to predict on?")]
        public void ExceptionThrownWhenInputDataValuesAreSame()
        {
            double[][] dummyData = new double[4][];
            for (int i = 0; i < dummyData.Length; i++)
            {
                dummyData[i] = new double[1];
                dummyData[i][0] = 25.00;
            }

            Normalizations.GaussNormal(dummyData, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "input data for Gaussian normalization is null.")]
        public void ExceptionThrownWhenInputDataIsNull()
        {
            Normalizations.GaussNormal(null, 0);
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
            Normalizations.GaussNormal(dummyData, -1);
        }
    }
}
