using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Hw12;

//BenchmarkRunner.Run<WebApplicationWorkingTimeTests>();
BenchmarkSwitcher.FromAssembly(typeof(WebApplicationWorkingTimeTests).Assembly).Run(args, new DebugInProcessConfig());