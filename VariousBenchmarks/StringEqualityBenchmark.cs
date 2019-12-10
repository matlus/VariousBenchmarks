using BenchmarkDotNet.Attributes;
using System;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class StringEqualityBenchmark
    {
        private string stringA = null;
        private string stringB = null;
        private readonly StringComparer stringComparerOrdinal = StringComparer.Ordinal;
        private readonly StringComparer stringComparerOrdinalIgnoreCase = StringComparer.OrdinalIgnoreCase;

        [Params(10, 100/*, 200, 300, 500, 1000*/)]
        public int StringLength { get; set; }

        [GlobalSetup]
        public void Initialize()
        {
            stringA = Randomizer.GetRandomAsciiString(StringLength);
            stringB = stringA + Randomizer.GetRandomAsciiString(1);
        }

        [Benchmark]
        public bool EqualsOperator()
        {
            return stringA == stringB;
        }

        [Benchmark]
        public bool EqualsMethod()
        {
            return stringA.Equals(stringB);
        }

        [Benchmark]
        public bool EqualsOrdinalCompare()
        {
            return stringA.Equals(stringB, StringComparison.Ordinal);
        }

        [Benchmark]
        public bool EqualsOrdinalIgnoreCase()
        {
            return stringA.Equals(stringB, StringComparison.OrdinalIgnoreCase);
        }

        [Benchmark]
        public bool EqualsInvariantCulture()
        {
            return stringA.Equals(stringB, StringComparison.InvariantCulture);
        }

        [Benchmark]
        public bool EqualsInvariantCultureIgnoreCase()
        {
            return stringA.Equals(stringB, StringComparison.InvariantCultureIgnoreCase);
        }

        [Benchmark]
        public bool StringComparerOrdinal()
        {
            return stringComparerOrdinal.Equals(stringA, stringB);
        }

        [Benchmark]
        public bool StringComparerOrdinalIgnoreCase()
        {
            return stringComparerOrdinalIgnoreCase.Equals(stringA, stringB);
        }
    }
}
