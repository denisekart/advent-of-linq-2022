/// This file was generated automatically by a tool.
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using BenchmarkDotNet.Attributes;

public class Day10
{
    private string data = null!;
    Action<string>
        part1 = null!,
        part2 = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        data = DataExtensions.GetDataForDay(10) ?? throw new ArgumentNullException(nameof(data));

        var testClass = new AdventOfCode22Tests.Day10();
        part1 = testClass.Part1_ShouldReturnValidSolution;
        part2 = testClass.Part2_ShouldReturnValidSolution;
    }

    [Benchmark]
    public void Benchmark_Part1()
    {
        part1(data);
    }

    [Benchmark]
    public void Benchmark_Part2()
    {
        part2(data);
    }
}
