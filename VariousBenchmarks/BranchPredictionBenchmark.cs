using BenchmarkDotNet.Attributes;
using System.Linq;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class BranchPredictionBenchmark
    {
        private const int MAX_NUMBER = 1000000;
        private const int _MID_POINT = MAX_NUMBER / 2;
        private int[] orderedNumbers;
        private int[] randomOrderedNumbers;

        [GlobalSetup]
        public void GlobalSetup()
        {
            orderedNumbers = Enumerable.Range(0, MAX_NUMBER).ToArray();
            randomOrderedNumbers = Randomizer.ShuffleArray(orderedNumbers);
        }

        [Benchmark]
        public int IterateOverOrderedArray()
        {
            var higherThanMidpoint = 0;

            for (int i = 0; i < orderedNumbers.Length; i++)
            {
                if (orderedNumbers[i] > _MID_POINT)
                {
                    higherThanMidpoint++;
                }
            }

            return higherThanMidpoint;
        }

        [Benchmark(Baseline = true)]
        public int IterateOverRandomOrderedArray()
        {
            var higherThanMidpoint = 0;

            for (int i = 0; i < randomOrderedNumbers.Length; i++)
            {
                if (randomOrderedNumbers[i] > _MID_POINT)
                {
                    higherThanMidpoint++;
                }
            }

            return higherThanMidpoint;
        }
    }
}
