using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public static class Program
    {
        static void Main()
        {
            ////var vtb = new ReferenceTypeBenchmark();
            ////vtb.NumberOfElements = 10;
            ////vtb.Initialize();

            ////BenchmarkRunner.Run<ValueTypeBenchmark>();
            ////BenchmarkRunner.Run<ReferenceTypeBenchmark>();
            ////BenchmarkRunner.Run<StringEqualityBenchmark>();
            ////BenchmarkRunner.Run<StringConcatenationBenchmark>();
            ////BenchmarkRunner.Run<BranchPredictionBenchmark>();
            BenchmarkRunner.Run<LinqWhereBenchMarks>();
            ////BenchmarkRunner.Run<AsyncVsContinueWith>();
            

            ////var linqWhereBenchMarks = new LinqWhereBenchMarks();
            ////linqWhereBenchMarks.GlobalSetup();
            ////linqWhereBenchMarks.GetUsingMergeSetsAndSingleToUpper();

            Console.WriteLine("Done.....Press any key to Quit");
            Console.ReadLine();
        }
    }
}
