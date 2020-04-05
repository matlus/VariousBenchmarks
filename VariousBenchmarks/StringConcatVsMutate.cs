using BenchmarkDotNet.Attributes;
using System;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class StringConcatVsMutate
    {
        [Benchmark(Baseline = true)]
        public string ConcatStringsPlusOperator()
        {
            var s1 = "Hello";
            var s2 = " World.";
            var s3 = " How";
            var s4 = " Are";
            var s5 = " You";
            var s6 = " Doing?";
            return s1 + s2 + s3 + s4 + s5 + s6;
        }

        [Benchmark]
        public string ConcatStringsFormatMethod()
        {
            var s1 = "Hello";
            var s2 = " World.";
            var s3 = " How";
            var s4 = " Are";
            var s5 = " You";
            var s6 = " Doing?";            
            return string.Format("{0}{1}{2}{3}{4}{5}", s1, s2, s3, s4, s5, s6);
        }

        [Benchmark]
        public string ConcatStringsInterpolation()
        {
            var s1 = "Hello";
            var s2 = " World.";
            var s3 = " How";
            var s4 = " Are";
            var s5 = " You";
            var s6 = " Doing?";
            return $"{s1}{s2}{s3}{s4}{s5}{s6}";
        }


        [Benchmark]
        public string MutateString()
        {
            var s1 = "Hello";
            s1 += " World.";
            s1 += " How";
            s1 += " Are";
            s1 += " You";
            s1 += " Doing?";
            return s1;
        }
    }
}
