using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

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

            ////BenchmarkRunner.Run<ArrayDictValueTypeBenchmark>();
            ////BenchmarkRunner.Run<ReferenceTypeBenchmark>();
            ////BenchmarkRunner.Run<StringEqualityBenchmark>();
            ////BenchmarkRunner.Run<StringConcatVsMutate>();
            ////BenchmarkRunner.Run<StringConcatenationBenchmark>();
            ////BenchmarkRunner.Run<BranchPredictionBenchmark>();
            ////BenchmarkRunner.Run<AsyncVsContinueWith>();
            BenchmarkRunner.Run<LinqWhereBenchMarks>();
            
            Console.WriteLine("Done.....Press any key to Quit");
            Console.ReadLine();
        }
    }
}
