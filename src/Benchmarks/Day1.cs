// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;

public class Day1
{
    private string data = null!;
    Action<string> 
        part1 = null!, 
        part2 = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        data = DataExtensions.GetDataForDay(1) ?? throw new ArgumentNullException(nameof(data));

        var testClass = new AdventOfCode22Tests.Day1();
        part1 = testClass.Part1_ShouldReturnGroupWithMaxCaloriesWhenGivenAValidInputString;
        part2 = testClass.Part2_ShouldReturnSumOfTop3GroupsWithMostCaloriesWhenGivenAValidInputString;
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
