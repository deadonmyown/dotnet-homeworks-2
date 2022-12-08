using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Hw12;

BenchmarkSwitcher.FromAssembly(typeof(WebApplicationWorkingTimeTests).Assembly).Run(args, new DebugInProcessConfig());