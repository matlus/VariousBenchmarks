using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class ArrayDictReferenceTypeBenchmark
    {
        private SomeClass[] someClassArray;
        private SomeClass[] sortedSomeClassArray;
        private Dictionary<string, SomeClass> someClassDictionary;
        private string[] namesToLookup;
        private readonly StringComparer stringComparerOrdinal = StringComparer.Ordinal;
        private readonly StringComparer stringComparerOrdinalIgnoreCase = StringComparer.OrdinalIgnoreCase;

        [Params(5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80)]
        public int NumberOfElements { get; set; }

        [GlobalSetup]
        public void Initialize()
        {
            someClassArray = new SomeClass[NumberOfElements];
            sortedSomeClassArray = new SomeClass[NumberOfElements];
            someClassDictionary = new Dictionary<string, SomeClass>();
            namesToLookup = new string[NumberOfElements];

            for (int i = 0; i < NumberOfElements; i++)
            {
                var randomName = Randomizer.GetRandomAsciiString(10);
                namesToLookup[i] = randomName;

                var someClass = new SomeClass(randomName, Randomizer.GetRandomAsciiString(30));
                someClassArray[i] = someClass;
                sortedSomeClassArray[i] = someClass;
                someClassDictionary.Add(someClass.Name, someClass);                
            }

            Array.Sort(sortedSomeClassArray, (item1, item2) => item1.Name.CompareTo(item2.Name));            

            namesToLookup = Randomizer.ShuffleArray(namesToLookup);
        }

        [Benchmark(Baseline = true)]
        public SomeClass ArrayLookup()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                for (int j = 0; j < someClassArray.Length; j++)
                {
                    if (someClassArray[j].Name == namesToLookup[i])
                    {
                        found = someClassArray[j];
                    }
                }
            }

            return found;
        }

        [Benchmark]
        public SomeClass ArrayLookupOrdinal()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                for (int j = 0; j < someClassArray.Length; j++)
                {                    
                    if (someClassArray[j].Name.Equals(namesToLookup[i], StringComparison.Ordinal))
                    {
                        found = someClassArray[j];
                    }
                }
            }

            return found;
        }

        [Benchmark]
        public SomeClass ArrayLookupStringComparerOrdinal()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                for (int j = 0; j < someClassArray.Length; j++)
                {
                    if (stringComparerOrdinal.Equals(someClassArray[j].Name, namesToLookup[i]))
                    {
                        found = someClassArray[j];
                    }
                }
            }

            return found;
        }

        [Benchmark]
        public SomeClass ArrayLookupStringComparerOrdinalIgnoreCase()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                for (int j = 0; j < someClassArray.Length; j++)
                {
                    if (stringComparerOrdinalIgnoreCase.Equals(someClassArray[j].Name, namesToLookup[i]))
                    {
                        found = someClassArray[j];
                    }
                }
            }

            return found;
        }

        [Benchmark]
        public SomeClass SortedArrayLookup()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                found = Array.Find(sortedSomeClassArray, sc => sc.Name == namesToLookup[i]);
            }
            
            return found;
        }

        [Benchmark]
        public SomeClass DictionaryLookup()
        {
            SomeClass found = null;

            for (int i = 0; i < namesToLookup.Length; i++)
            {
                var nameToLookup = namesToLookup[i];
                if (someClassDictionary.ContainsKey(nameToLookup))
                {
                    found = someClassDictionary[nameToLookup];
                }
            }

            return found;
        }
    }

    public class SomeClass
    {
        public string Name { get; }
        public string Description { get; }

        public SomeClass(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
