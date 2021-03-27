using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser, RankColumn]
    public class BranchPredictionBenchmark
    {
        private const int MAX_NUMBER = 1000000;
        private const int _MID_POINT = MAX_NUMBER / 2;
        private int[] orderedNumbers;
        private int[] randomOrderedNumbers;
        private int[] randomOrderedNumbers2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            orderedNumbers = Enumerable.Range(0, MAX_NUMBER).ToArray();
            randomOrderedNumbers = Randomizer.ShuffleArray(orderedNumbers);
            randomOrderedNumbers2 = new int[randomOrderedNumbers.Length];
            Array.Copy(randomOrderedNumbers, randomOrderedNumbers2, randomOrderedNumbers.Length);
        }

        [Benchmark(Baseline = true)]
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

        [Benchmark]
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

        [Benchmark]
        public int RandomOrderedArrayLinqWhere()
        {
            return randomOrderedNumbers.Count(n => n > _MID_POINT);
        }

        [Benchmark]
        public int SortRandomOrderedArrayAndIterate()
        {
            Array.Sort(randomOrderedNumbers2);
            var higherThanMidpoint = 0;

            for (int i = 0; i < randomOrderedNumbers2.Length; i++)
            {
                if (randomOrderedNumbers2[i] > _MID_POINT)
                {
                    higherThanMidpoint++;
                }
            }

            return higherThanMidpoint;
        }
    }
}
