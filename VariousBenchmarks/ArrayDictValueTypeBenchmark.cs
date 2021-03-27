using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace ArrayVsDictionaryBenchmark
{
    [CsvExporter]
    [CsvMeasurementsExporter]
    [MemoryDiagnoser]
    public class ArrayDictValueTypeBenchmark
    {
        private int[] intArray;
        private int[] intArraySorted;
        private Dictionary<int, int> intDictionary;
        private int[] numbersToLookup;

        [Params(30, 55, /*60, 65, 70, 75,*/ 80)]
        public int NumberOfElements { get; set; }

        [GlobalSetup]
        public void Initialize()
        {
            intArray = Randomizer.GetRandomUniqueInts(NumberOfElements);
            intArraySorted = new int[intArray.Length];
            intDictionary = new Dictionary<int, int>();

            for (int i = 0; i < intArray.Length; i++)
            {
                intArraySorted[i] = intArray[i];
                intDictionary.Add(intArray[i], intArray[i]);
            }

            Array.Sort(intArraySorted);

            numbersToLookup = Randomizer.ShuffleArray(intArray);
        }

        [Benchmark(Baseline = true)]
        public int ArrayLookup()
        {
            var foundCount = 0;

            for (int i = 0; i < numbersToLookup.Length - 1; i++)
            {
                for (int j = 0; j < intArray.Length - 1; j++)
                {
                    if (intArray[j] == numbersToLookup[i])
                    {
                        foundCount++;
                        break;
                    }
                }
            }

            return foundCount;
        }

        [Benchmark]
        public int SortedArrayLookup()
        {
            var foundCount = 0;
            for (int i = 0; i < numbersToLookup.Length - 1; i++)
            {
                _ = Array.BinarySearch<int>(intArraySorted, numbersToLookup[i]);
                foundCount++;
            }

            return foundCount;
        }

        [Benchmark]
        public int DictionaryLookup()
        {
            var foundCount = 0;

            for (int i = 0; i < numbersToLookup.Length - 1; i++)
            {
                _ = intDictionary[numbersToLookup[i]];
                foundCount++;
            }

            return foundCount;
        }

        [Benchmark]
        public int DictionaryLookupContains()
        {
            var foundCount = 0;

            for (int i = 0; i < numbersToLookup.Length - 1; i++)
            {
                if (intDictionary.ContainsKey(numbersToLookup[i]))
                {
                    _ = intDictionary[numbersToLookup[i]];
                    foundCount++;
                }
            }

            return foundCount;
        }

        [Benchmark]
        public int DictionaryTryGetValue()
        {
            var foundCount = 0;

            for (int i = 0; i < numbersToLookup.Length - 1; i++)
            {
                if (intDictionary.TryGetValue(numbersToLookup[i], out int value))
                {
                    foundCount++;
                }
            }

            return foundCount;
        }
    }
}
