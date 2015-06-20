using System.Collections.Generic;
using System.IO;

namespace Normalize
{
    public class CoDec
    {
        public static void EncodeFile(string originalFile, string encodedFile, int column,
         string encodingType)
        {
            // encodingType: "effects" or "dummy"
            FileStream ifs = new FileStream(originalFile, FileMode.Open);
            StreamReader sr = new StreamReader(ifs);
            string line = "";
            string[] tokens = null;
            // count distinct items in column
            Dictionary<string, int> d = new Dictionary<string, int>();
            int itemNum = 0;
            while ((line = sr.ReadLine()) != null)
            {
                tokens = line.Split(','); // Assumes items are comma-delimited.
                if (d.ContainsKey(tokens[column]) == false)
                    d.Add(tokens[column], itemNum++);
            }
            sr.Close();
            ifs.Close();
            // Replace items in the column.
            int N = d.Count; // Number of distinct strings.

            ifs = new FileStream(originalFile, FileMode.Open);
            sr = new StreamReader(ifs);
            FileStream ofs = new FileStream(encodedFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(ofs);
            string s = null; // result string/line
            while ((line = sr.ReadLine()) != null)
            {
                s = "";
                tokens = line.Split(','); // Break apart.
                for (int i = 0; i < tokens.Length; ++i) // Reconstruct.
                {
                    if (i == column) // Encode this string.
                    {
                        int index = d[tokens[i]]; // 0, 1, 2 or . .
                        if (encodingType == "effects")
                            s += Encodings.EffectsEncoding(index, N) + ",";
                        else if (encodingType == "dummy")
                            s += Encodings.DummyEncoding(index, N) + ",";
                    }
                    else
                        s += tokens[i] + ",";
                }
                s.Remove(s.Length - 1); // Remove trailing ',' .
                sw.WriteLine(s); // Write the string to file.
            } // while
            sw.Close(); ofs.Close();
            sr.Close(); ifs.Close();
        }
    }
}
