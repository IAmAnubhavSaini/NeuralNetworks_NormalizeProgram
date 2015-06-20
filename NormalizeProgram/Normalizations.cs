using System;

namespace Normalize
{
    public class Normalizations
    {
        public static void GaussNormal(double[][] data, int column)
        {
            SanityCheck(data, "Input data for Gaussian normalization is null.", column, "Input `column` cannot be less than zero.");
            double sum = FindSum(data, column);
            double mean = sum / data.Length;
            double sumSquares = FindSumOfSquares(data, column, mean);
            double stdDev = Math.Sqrt(sumSquares / data.Length);
            SanityCheckForRange(stdDev);
            SetupNormalizedData(data, column, mean, stdDev);
        }

        private static void SanityCheckForRange(double stdDev)
        {
            if (Math.Abs(stdDev) < 0.0000001)
            {
                throw new ArgumentException("All the values entered are same, is it even a good input to predict on?");
            }
        }

        private static void SetupNormalizedData(double[][] data, int column, double mean, double stdDev)
        {
            for (var i = 0; i < data.Length; ++i)
                data[i][column] = (data[i][column] - mean) / stdDev;
        }

        private static double FindSumOfSquares(double[][] data, int column, double mean)
        {
            double sumSquares = 0.0;
            for (int i = 0; i < data.Length; ++i)
                sumSquares += (data[i][column] - mean) * (data[i][column] - mean);
            return sumSquares;
        }

        private static double FindSum(double[][] data, int column)
        {
            double sum = 0.0;
            for (int i = 0; i < data.Length; ++i)
                sum += data[i][column];
            return sum;
        }

        public static void MinMaxNormal(double[][] data, int column)
        {
            SanityCheck(data, "Input data for Min-Max normalization is null.", column, "Input `column` cannot be less than zero.");
            double min = FindMin(data, column);
            double max = FindMax(data, column);
            double range = max - min;
            if (range == 0.0) // ugly
            {
                SetDefaultValueInNoStandardDeviationCase(data, column);
                return;
            }
            for (int i = 0; i < data.Length; ++i)
                data[i][column] = (data[i][column] - min) / range;
        }

        private static double FindMin(double[][] data, int column)
        {
            return FindInData(data, column, (x, y) => x < y);
        }

        private static double FindMax(double[][] data, int column)
        {

            return FindInData(data, column, (x, y) => x > y);
        }

        private delegate bool FunctionForFinding(double a, double b);
        
        private static double FindInData(double[][] data, int column, FunctionForFinding firstArgIsFavorable)
        {
            double find = data[0][column];
            for (int i = 0; i < data.Length; ++i)
            {
                if (firstArgIsFavorable(data[i][column], find))
                {
                    find = data[i][column];
                }
            }
            return find;
        }

        private static void SetDefaultValueInNoStandardDeviationCase(double[][] data, int j)
        {
            for (int i = 0; i < data.Length; ++i)
                data[i][j] = 0.5;
        }

        private static void SanityCheck(double[][] data, string dataMessage, int column, string columnMessage)
        {
            if (data == null)
                throw new ArgumentNullException("data", dataMessage);
            if (column < 0)
                throw new ArgumentOutOfRangeException("column", columnMessage);
        }
    }
}
