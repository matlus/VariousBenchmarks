using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public static class Program
    {
        static void Main(string[] args)
        {
            ////var vtb = new ReferenceTypeBenchmark();
            ////vtb.NumberOfElements = 10;
            ////vtb.Initialize();

            ////BenchmarkRunner.Run<ValueTypeBenchmark>();
            ////BenchmarkRunner.Run<ReferenceTypeBenchmark>();
            ////BenchmarkRunner.Run<StringEqualityBenchmark>();
            ////BenchmarkRunner.Run<StringConcatenationBenchmark>();
            ////BenchmarkRunner.Run<BranchPredictionBenchmark>();
            BenchmarkRunner.Run<AsyncVsContinueWith>();

            Console.WriteLine("Done.....Press any key to Quit");
            Console.ReadLine();
        }
    }
}
