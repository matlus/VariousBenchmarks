using BenchmarkDotNet.Attributes;
using System.Text;

namespace ArrayVsDictionaryBenchmark
{
    [CsvExporter]
    [MemoryDiagnoser]
    public class StringConcatenationBenchmark
    {
        private string[] stringsToConcat;

        [Params(2, 3, 4, 5, 6, 7, 8, 9, 10)]
        public int NumberOfStrings { get; set; }

        [Params(50)]
        public int StringLength { get; set; }

        [GlobalSetup]
        public void Initialize()
        {
            stringsToConcat = new string[NumberOfStrings];
            for (int i = 0; i < NumberOfStrings; i++)
            {
                stringsToConcat[i] = Randomizer.GetRandomAsciiString(StringLength);
            }
        }

        [Benchmark]
        public string PlusOperator()
        {
            string concatenatedString = null;

            switch (NumberOfStrings)
            {
                case 2:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1];
                    break;
                case 3:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2];
                    break;
                case 4:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3];
                    break;
                case 5:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4];
                    break;
                case 6:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4] + stringsToConcat[5];
                    break;
                case 7:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4] + stringsToConcat[5] + stringsToConcat[6];
                    break;
                case 8:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4] + stringsToConcat[5] + stringsToConcat[6] + stringsToConcat[7];
                    break;
                case 9:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4] + stringsToConcat[5] + stringsToConcat[6] + stringsToConcat[7] + stringsToConcat[8];
                    break;
                case 10:
                    concatenatedString = stringsToConcat[0] + stringsToConcat[1] + stringsToConcat[2] + stringsToConcat[3] + stringsToConcat[4] + stringsToConcat[5] + stringsToConcat[6] + stringsToConcat[7] + stringsToConcat[8] + stringsToConcat[9];
                    break;
                default:
                    break;
            }

            return concatenatedString;
        }

        [Benchmark]
        public string PlusOperatorLoop()
        {
            string concatenatedString = null;

            for (int i = 0; i < NumberOfStrings; i++)
            {
                concatenatedString += stringsToConcat[i];
            }

            return concatenatedString;
        }

        [Benchmark]
        public string ConcatMethod()
        {
            string concatenatedString;

            switch (NumberOfStrings)
            {
                case 2:
                    concatenatedString = string.Concat(stringsToConcat[0], stringsToConcat[1]);
                    break;
                case 3:
                    concatenatedString = string.Concat(stringsToConcat[0], stringsToConcat[1], stringsToConcat[2]);
                    break;
                case 4:
                    concatenatedString = string.Concat(stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3]);
                    break;
                default:
                    concatenatedString = string.Concat(stringsToConcat);
                    break;
            }

            return concatenatedString;
        }

        [Benchmark]
        public string ConcatLoop()
        {
            string concatenatedString = null;

            for (int i = 0; i < NumberOfStrings; i++)
            {
                concatenatedString = string.Concat(concatenatedString, stringsToConcat[i]);
            }

            return concatenatedString;
        }

        [Benchmark]
        public string StringFormat()
        {
            string concatenatedString = null;

            switch (NumberOfStrings)
            {
                case 2:
                    concatenatedString = string.Format("{0} {1}", stringsToConcat[0], stringsToConcat[1]);
                    break;
                case 3:
                    concatenatedString = string.Format("{0} {1} {2}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2]);
                    break;
                case 4:
                    concatenatedString = string.Format("{0} {1} {2} {3}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3]);
                    break;
                case 5:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4]);
                    break;
                case 6:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4} {5}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4], stringsToConcat[5]);
                    break;
                case 7:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4} {5} {6}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4], stringsToConcat[5], stringsToConcat[6]);
                    break;
                case 8:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4} {5} {6} {7}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4], stringsToConcat[5], stringsToConcat[6], stringsToConcat[7]);
                    break;
                case 9:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4], stringsToConcat[5], stringsToConcat[6], stringsToConcat[7], stringsToConcat[8]);
                    break;
                case 10:
                    concatenatedString = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", stringsToConcat[0], stringsToConcat[1], stringsToConcat[2], stringsToConcat[3], stringsToConcat[4], stringsToConcat[5], stringsToConcat[6], stringsToConcat[7], stringsToConcat[8], stringsToConcat[9]);
                    break;
            }

            return concatenatedString;
        }


        [Benchmark]
        public string StringBuilder()
        {
            var stringBuilder = new StringBuilder();

            switch (NumberOfStrings)
            {
                case 2:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    break;
                case 3:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    break;
                case 4:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    break;
                case 5:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    break;
                case 6:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    stringBuilder.Append(stringsToConcat[5]);
                    break;
                case 7:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    stringBuilder.Append(stringsToConcat[5]);
                    stringBuilder.Append(stringsToConcat[6]);
                    break;
                case 8:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    stringBuilder.Append(stringsToConcat[5]);
                    stringBuilder.Append(stringsToConcat[6]);
                    stringBuilder.Append(stringsToConcat[7]);
                    break;
                case 9:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    stringBuilder.Append(stringsToConcat[5]);
                    stringBuilder.Append(stringsToConcat[6]);
                    stringBuilder.Append(stringsToConcat[7]);
                    stringBuilder.Append(stringsToConcat[8]);
                    break;
                case 10:
                    stringBuilder.Append(stringsToConcat[0]);
                    stringBuilder.Append(stringsToConcat[1]);
                    stringBuilder.Append(stringsToConcat[2]);
                    stringBuilder.Append(stringsToConcat[3]);
                    stringBuilder.Append(stringsToConcat[4]);
                    stringBuilder.Append(stringsToConcat[5]);
                    stringBuilder.Append(stringsToConcat[6]);
                    stringBuilder.Append(stringsToConcat[7]);
                    stringBuilder.Append(stringsToConcat[8]);
                    stringBuilder.Append(stringsToConcat[9]);
                    break;
                default:
                    break;
            }

            return stringBuilder.ToString();
        }

        [Benchmark]
        public string StringBuilderLoop()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < NumberOfStrings; i++)
            {
                stringBuilder.Append(stringsToConcat[i]);
            }

            return stringBuilder.ToString();
        }

        [Benchmark]
        public string StringBuilderCachedLoop()
        {            
            var stringBuilder = StringBuilderCache.Acquire(NumberOfStrings * StringLength * 2);

            for (int i = 0; i < NumberOfStrings; i++)
            {
                stringBuilder.Append(stringsToConcat[i]);
            }

            return StringBuilderCache.GetStringAndRelease(stringBuilder);
        }

        [Benchmark]
        public string StringJoin()
        {
            return string.Join('\0', stringsToConcat);
        }
    }
}
