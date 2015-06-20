using System;
using System.Collections.Generic;

namespace Normalize
{
    class NormalizeProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin data encoding and normalization demo\n");
            string[] sourceData = new string[] {
                "Sex Age Locale Income Politics",
                "==============================================",
                "Male 25 Rural 63,000.00 Conservative",
                "Female 36 Suburban 55,000.00 Liberal",
                "Male 40 Urban 74,000.00 Moderate",
                "Female 23 Rural 28,000.00 Liberal" 
            };
            Console.WriteLine("Dummy data in raw form: \n");
            ShowData(sourceData);

            var encodedData = new string[] {
                "- 1 25 1 0 63,000.00 1 0 0" ,
                " 1 36 0 1 55,000.00 0 1 0" ,
                "- 1 40 - 1 -1 74,000.00 0 0 1" ,
                " 1 23 1 0 28,000.00 0 1 0" 
            };

            //Encode(".. \\.. \\Politics.txt", ".. \\.. \\PoliticsEncoded.txt", 4, "dummy");
            Console.WriteLine("\nData after categorical encoding: \n");
            ShowData(encodedData);
            Console.WriteLine("\nNumeric data stored in matrix: \n");
            var numericData = new double[4][];
            numericData[0] = new double[] { -1, 25.0, 1, 0, 63000.00, 1, 0, 0 };
            numericData[1] = new double[] { 1, 36.0, 0, 1, 55000.00, 0, 1, 0 };
            numericData[2] = new double[] { -1, 40.0, -1, -1, 74000.00, 0, 0, 1 };
            numericData[3] = new double[] { 1, 23.0, 1, 0, 28000.00, 0, 1, 0 };
            ShowMatrix(numericData, 2);
            GaussNormal(numericData, 1);
            MinMaxNormal(numericData, 4);
            Console.WriteLine("\nMatrix after normalization (Gaussian col. 1" +
            " and MinMax col. 4): \n");
            ShowMatrix(numericData, 2);

            Console.WriteLine("\nEnd data encoding and normalization demo\n");

        } // Main

        static void GaussNormal(double[][] data, int column)
        {
            // sanity check
            if (data == null)
            {
                throw new ArgumentNullException("data", "input data for Gaussian normalization is null.");
            }
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

        static void MinMaxNormal(double[][] data, int column)
        {
            // sanity check
            if (data == null)
            {
                throw new ArgumentNullException("data", "input data for Min-Max normalization is null.");
            }
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

        static void ShowMatrix(double[][] matrix, int decimals)
        {
            // sanity check
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix", "matrix cannot be null.");
            }
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    double v = Math.Abs(matrix[i][j]);
                    if (matrix[i][j] >= 0.0)
                        Console.Write(" ");
                    else
                        Console.Write("-");
                    Console.Write(v.ToString("F" + decimals).PadRight(5) + " ");
                }
                Console.WriteLine("");
            }
        }
        static void ShowData(IEnumerable<string> rawData)
        {
            foreach (var t in rawData)
                Console.WriteLine(t);
            Console.WriteLine("");
        }

        //static void EncodeFile(string originalFile, string encodedFile, int column,
        // string encodingType)
        //{
        // // encodingType: "effects" or "dummy"
        // FileStream ifs = new FileStream(originalFile, FileMode.Open);
        // StreamReader sr = new StreamReader(ifs);
        // string line = "";
        // string[] tokens = null;
        // // count distinct items in column
        // Dictionary<string, int> d = new Dictionary<string,int>();
        // int itemNum = 0;
        // while ((line = sr.ReadLine()) != null)
        // {
        //  tokens = line.Split(','); // Assumes items are comma-delimited.
        // if (d.ContainsKey(tokens[column]) == false)
        // d.Add(tokens[column], itemNum++);
        // }
        // sr.Close();
        // ifs.Close();
        // // Replace items in the column.
        // int N = d.Count; // Number of distinct strings.

        // ifs = new FileStream(originalFile, FileMode.Open);
        // sr = new StreamReader(ifs);
        // FileStream ofs = new FileStream(encodedFile, FileMode.Create);
        // StreamWriter sw = new StreamWriter(ofs);
        // string s = null; // result string/line
        // while ((line = sr.ReadLine()) != null)
        // {
        // s = "";
        // tokens = line.Split(','); // Break apart.
        // for (int i = 0; i < tokens.Length; ++i) // Reconstruct.
        // {
        // if (i == column) // Encode this string.
        // {
        // int index = d[tokens[i]]; // 0, 1, 2 or . .
        // if (encodingType == "effects")
        // s += EffectsEncoding(index, N) + ",";
        // else if (encodingType == "dummy")
        // s += DummyEncoding(index, N) + ",";
        // }
        // else
        // s += tokens[i] +",";
        // }
        // s.Remove(s.Length - 1); // Remove trailing ',' .
        // sw.WriteLine(s); // Write the string to file.
        // } // while
        // sw.Close(); ofs.Close();
        // sr.Close(); ifs.Close();
        //}
        static string EffectsEncoding(int index, int N)
        {
            // If N = 3 and index = 0 -> 1,0.
            // If N = 3 and index = 1 -> 0,1.
            // If N = 3 and index = 2 -> -1, -1.
            if (N == 2) // Special case.
            {
                if (index == 0) return "-1";
                else if (index == 1) return "1";
            }
            var values = new int[N - 1];
            if (index == N - 1) // Last item is all - 1s.
            {
                for (var i = 0; i < values.Length; ++i)
                    values[i] = -1;
            }
            else
            {
                values[index] = 1; // 0 values are already there.
            }
            var s = values[0].ToString();
            for (var i = 1; i < values.Length; ++i)
                s += ", " + values[i];
            return s;
        }

        static string DummyEncoding(int index, int N)
        {
            var values = new int[N];
            values[index] = 1;
            var s = values[0].ToString();
            for (var i = 1; i < values.Length; ++i)
                s += "," + values[i];
            return s;
        }
    } // NormalizeProgram class
} // ns
