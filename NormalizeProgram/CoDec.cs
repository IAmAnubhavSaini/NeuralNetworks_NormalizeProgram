using System;
using System.Collections.Generic;
using System.IO;

namespace Normalize
{
    public class CoDec
    {
        public static void EncodeFile(string originalFile, string encodedFile, int column,
         string encodingType)
        {
            string[] tokens = null;
            var d = new Dictionary<string, int>();
            int N = CountDistinctItemsInColumn(originalFile, column, tokens, d);
            WriteEncodedDataToOutputFile(originalFile, encodedFile, column, encodingType, tokens, d, N);
        }

        private static void WriteEncodedDataToOutputFile(string originalFile, string encodedFile, int column, string encodingType, string[] tokens, Dictionary<string, int> d, int N)
        {
            var ifs = new FileStream(originalFile, FileMode.Open);
            var sr = new StreamReader(ifs);
            var ofs = new FileStream(encodedFile, FileMode.Create);
            var sw = new StreamWriter(ofs);
            
            var line = "";
            while ((line = sr.ReadLine()) != null)
            {
                var result = ReconstructStringFromEncodedData(column, encodingType, line.Split(','), d, N);
                RemoveTrailingComma(result);
                sw.WriteLine(result);
            }
            sw.Close(); ofs.Close();
            sr.Close(); ifs.Close();
        }

        private static string ReconstructStringFromEncodedData(int column, string encodingType, string[] tokens, Dictionary<string, int> d, int N)
        {
            string result = "";
            for (int i = 0; i < tokens.Length; ++i)
            {
                if (i == column)
                    result += EncodeCurrentString(encodingType, N, d[tokens[i]]);
                else
                    result += tokens[i] + ",";
            }
            return result;
        }

        private static int CountDistinctItemsInColumn(string originalFile, int column, string[] tokens, Dictionary<string, int> d)
        {
            var ifs = new FileStream(originalFile, FileMode.Open);
            var sr = new StreamReader(ifs);
            var line = "";
            var itemNum = 0;
            while ((line = sr.ReadLine()) != null)
            {
                tokens = line.Split(',');
                if (d.ContainsKey(tokens[column]) == false)
                    d.Add(tokens[column], itemNum++);
            }
            sr.Close();
            ifs.Close();
            
            return d.Count;
        }

        private static void RemoveTrailingComma(string s)
        {
            s.Remove(s.Length - 1);
        }

        private static string EncodeCurrentString(string encodingType, int N, int index)
        {
            if (encodingType == "effects")
                return Encodings.EffectsEncoding(index, N) + ",";
            else if (encodingType == "dummy")
                return Encodings.DummyEncoding(index, N) + ",";
            else
                throw new ArgumentException("Wrong value received for encoding type.", "encodingType");
        }
    }
}
