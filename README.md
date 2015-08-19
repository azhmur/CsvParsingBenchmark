# CsvParsingBenchmark

// BenchmarkDotNet=v0.7.7.0

// OS=Microsoft Windows NT 6.1.7601 Service Pack 1

// Processor=Intel(R) Core(TM) i7-2600 CPU @ 3.40GHz, ProcessorCount=8

// Host CLR=MS.NET 4.0.30319.42000, Arch=64-bit  [RyuJIT] Common:  Type=Program  Mode=Throughput  Platform=HostPlatform  Jit=HostJit  .NET=HostFramework

          Method |   AvrTime |      StdDev |       op/s |
---------------- |---------- |------------ |----------- |
       CsvHelper |   2.18 us | 0.000000 ns |  457749.01 |
          Regexp |   6.38 us | 0.000000 ns |  156738.43 |
           Split | 320.36 ns | 0.000000 ns | 3121463.29 |
         Sprache |  65.42 us | 0.000000 ns |   15286.88 |
 TextFieldParser |  44.96 us | 0.000000 ns |   22239.57 |