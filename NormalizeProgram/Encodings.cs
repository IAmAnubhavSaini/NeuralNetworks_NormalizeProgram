using System;
namespace Normalize
{
    public class Encodings
    {
        public static string EffectsEncoding(int index, int N)
        {
            if (N == 2)
                return SpecialCaseForEffectsEncoding(index);

            var values = SetValuesBasedOnIndex(index, N);
            return CreateEncodedStringFromValues(values);
        }

        private static int[] SetValuesBasedOnIndex(int index, int N)
        {
            var values = InitializeEmptyIntegerArrayWithZeroes(N);
            if (index == N - 1)
                MakeLastValueAllNegativeOnesForEffectsEncoding(values);
            else
                SetValueForCurrentIndex(index, values);
            return values;
        }

        private static string CreateEncodedStringFromValues(int[] values)
        {
            string s = values[0].ToString();
            for (int i = 1; i < values.Length; ++i)
                s += ", " + values[i];
            return s;
        }

        private static void SetValueForCurrentIndex(int index, int[] values)
        {
            values[index] = 1;
        }

        private static int[] InitializeEmptyIntegerArrayWithZeroes(int N)
        {
            var values = new int[N - 1];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            return values;
        }

        private static void MakeLastValueAllNegativeOnesForEffectsEncoding(int[] values)
        {
            for (var i = 0; i < values.Length; ++i)
                values[i] = -1;
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
