using System;
using System.Collections.Generic;

namespace Normalize
{
    class NormalizeProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin data encoding and normalization demo\n");
            ProcessOriginalData();
            EncodeOriginalData();
            NormalizeEncodedData();
            Console.WriteLine("\nEnd data encoding and normalization demo\n");
            Console.ReadLine();
        }

        private static void EncodeOriginalData()
        {
            var encodedData = GenerateEncodedData();
            //Encode(".. \\.. \\Politics.txt", ".. \\.. \\PoliticsEncoded.txt", 4, "dummy");
            Console.WriteLine("\nData after categorical encoding: \n");
            ShowData(encodedData);
            Console.WriteLine("\nNumeric data stored in matrix: \n");
        }

        private static void ProcessOriginalData()
        {
            string[] sourceData = GenerateSourceData();
            Console.WriteLine("Dummy data in raw form: \n");
            ShowData(sourceData);
        }

        private static void NormalizeEncodedData()
        {
            var numericData = GetNumericData();
            PrintMatrix(numericData, 2);
            Normalizations.GaussNormal(numericData, 1);
            Normalizations.MinMaxNormal(numericData, 4);
            Console.WriteLine("\nMatrix after normalization (Gaussian col. 1" +
            " and MinMax col. 4): \n");
            PrintMatrix(numericData, 2);
        }

        private static double[][] GetNumericData()
        {
            var numericData = new double[4][];
            numericData[0] = new double[] { -1, 25.0, 1, 0, 63000.00, 1, 0, 0 };
            numericData[1] = new double[] { 1, 36.0, 0, 1, 55000.00, 0, 1, 0 };
            numericData[2] = new double[] { -1, 40.0, -1, -1, 74000.00, 0, 0, 1 };
            numericData[3] = new double[] { 1, 23.0, 1, 0, 28000.00, 0, 1, 0 };
            return numericData;
        }

        private static string[] GenerateEncodedData()
        {
            var encodedData = new string[] {
                "-1 25  1  0 63,000.00 1 0 0",
                " 1 36  0  1 55,000.00 0 1 0",
                "-1 40 -1 -1 74,000.00 0 0 1",
                " 1 23  1  0 28,000.00 0 1 0"
            };
            return encodedData;
        }

        private static string[] GenerateSourceData()
        {
            string[] sourceData = new string[] {
                "Sex Age Locale Income Politics",
                "==============================================",
                "Male 25 Rural 63,000.00 Conservative",
                "Female 36 Suburban 55,000.00 Liberal",
                "Male 40 Urban 74,000.00 Moderate",
                "Female 23 Rural 28,000.00 Liberal"
            };
            return sourceData;
        }

        static void PrintMatrix(double[][] matrix, int decimals)
        {
            NullMatrixSanityCheck(matrix);
            PrintMatrixRows(matrix, decimals);
        }

        private static void PrintMatrixRows(double[][] matrix, int decimals)
        {
            for (int i = 0; i < matrix.Length; ++i)
            {
                PrintCurrentRow(matrix, decimals, i);
            }
        }

        private static void PrintCurrentRow(double[][] matrix, int decimals, int i)
        {
            for (int j = 0; j < matrix[i].Length; ++j)
            {
                PrintCurrentValue(matrix, decimals, i, j);
            }
            Console.WriteLine("");
        }

        private static void PrintCurrentValue(double[][] matrix, int decimals, int i, int j)
        {
            double v = Math.Abs(matrix[i][j]);
            if (matrix[i][j] >= 0.0)
                Console.Write(" ");
            else
                Console.Write("-");
            Console.Write(v.ToString("F" + decimals).PadRight(5) + " ");
        }

        private static void NullMatrixSanityCheck(double[][] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix", "matrix cannot be null.");
            }
        }
        static void ShowData(IEnumerable<string> rawData)
        {
            foreach (var t in rawData)
                Console.WriteLine(t);
            Console.WriteLine("");
        }
    }
}
