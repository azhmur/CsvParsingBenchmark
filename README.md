# CsvParsingBenchmark

// BenchmarkDotNet=v0.7.7.0

// OS=Microsoft Windows NT 6.1.7601 Service Pack 1

// Processor=Intel(R) Core(TM) i7-2600 CPU @ 3.40GHz, ProcessorCount=8

// Host CLR=MS.NET 4.0.30319.42000, Arch=64-bit  [RyuJIT] Common:  Type=Program  Mode=Throughput  Platform=HostPlatform  Jit=HostJit  .NET=HostFramework


    Method |   AvrTime |      StdDev |       op/s |
---------- |---------- |------------ |----------- |
 CsvHelper |   2.21 us | 0.000000 ns |  453492.72 |
    Regexp |   6.48 us | 0.000000 ns |   154209.4 |
     Split | 324.39 ns | 0.000000 ns | 3082691.57 |
   Sprache |  64.11 us | 0.000000 ns |   15599.28 |
