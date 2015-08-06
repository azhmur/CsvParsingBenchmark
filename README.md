# CsvParsingBenchmark

// BenchmarkDotNet=v0.7.7.0

// OS=Microsoft Windows NT 6.1.7601 Service Pack 1

// Processor=Intel(R) Core(TM) i7-2600 CPU @ 3.40GHz, ProcessorCount=8

// Host CLR=MS.NET 4.0.30319.42000, Arch=64-bit  [RyuJIT] Common:  Type=Program  Mode=Throughput  Platform=HostPlatform  Jit=HostJit  .NET=HostFramework


  Method |   AvrTime |      StdDev |       op/s |
-------- |---------- |------------ |----------- |
  Regexp |   6.24 us | 0.000000 ns |  160161.38 |
   Split | 310.31 ns | 0.000000 ns | 3222533.39 |
 Sprache |  60.85 us | 0.000000 ns |   16432.95 |
