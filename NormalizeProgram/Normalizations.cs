using System;

namespace Normalize
{
    public class Normalizations
    {
        public static void GaussNormal(double[][] data, int column)
        {
            // sanity check
            if (data == null)
            {
                throw new ArgumentNullException("data", "Input data for Gaussian normalization is null.");
            }
            if (column < 0)
                throw new ArgumentOutOfRangeException("column", "Input `column` cannot be less than zero.");

            int j = column; // Convenience.
            double sum = 0.0;
            for (int i = 0; i < data.Length; ++i)
                sum += data[i][j];
            double mean = sum / data.Length;
            double sumSquares = 0.0;
            for (int i = 0; i < data.Length; ++i)
                sumSquares += (data[i][j] - mean) * (data[i][j] - mean);
            double stdDev = Math.Sqrt(sumSquares / data.Length);
            // sanity check
            if (Math.Abs(stdDev) < 0.0000001)
            {
                throw new ArgumentException("All the values entered are same, is it even a good input to predict on?");
            }
            for (var i = 0; i < data.Length; ++i)
                data[i][j] = (data[i][j] - mean) / stdDev;
        }

        public static void MinMaxNormal(double[][] data, int column)
        {
            // sanity check
            if (data == null)
            {
                throw new ArgumentNullException("data", "Input data for Min-Max normalization is null.");
            }
            if (column < 0)
                throw new ArgumentOutOfRangeException("column", "Input `column` cannot be less than zero.");
            int j = column;
            double min = data[0][j];
            double max = data[0][j];
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i][j] < min)
                    min = data[i][j];
                if (data[i][j] > max)
                    max = data[i][j];
            }
            double range = max - min;
            if (range == 0.0) // ugly
            {
                for (int i = 0; i < data.Length; ++i)
                    data[i][j] = 0.5;
                return;
            }
            for (int i = 0; i < data.Length; ++i)
                data[i][j] = (data[i][j] - min) / range;
        }
    }
}
