// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, Santa!");

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
