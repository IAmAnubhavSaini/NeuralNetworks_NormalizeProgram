using System;
namespace Normalize
{
    public class Encodings
    {
        public static string EffectsEncoding(int index, int N)
        {
            // If N = 3 and index = 0 -> 1,0.
            // If N = 3 and index = 1 -> 0,1.
            // If N = 3 and index = 2 -> -1, -1.
            if (N == 2)
                return SpecialCaseForEffectsEncoding(index);

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

        private static string SpecialCaseForEffectsEncoding(int index)
        {
            if (index == 0) return "-1";
            else if (index == 1) return "1";
            else throw new ArgumentOutOfRangeException("index", "Values for index can only be 0 or 1.");
        }

        public static string DummyEncoding(int index, int N)
        {
            var values = new int[N];
            values[index] = 1;
            var s = values[0].ToString();
            for (var i = 1; i < values.Length; ++i)
                s += "," + values[i];
            return s;
        }
    }
}
