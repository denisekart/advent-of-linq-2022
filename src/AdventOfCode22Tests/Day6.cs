/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/6
/// </summary>
public class Day6
{
    [TestCase("""
        bvwbjplbgvbhsrlpgdmjqwftvncz
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-6-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var solution =
            input.Zip(input.Skip(1), input.Skip(2)).Zip(input.Skip(3))
            .Select((c, i) => (index: i, chars: new[] { c.First.First, c.First.Second, c.First.Third, c.Second }))
            .First(x => x.chars.Distinct().Count() == 4)
            .index + 4;

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        mjqjpqmgbljsphdztnvjfqwrcgsmlb
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-6-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var solution = input
            .Skip(13)
            .Select((_, i) => (index: i, chars: input.Substring(i, 14)))
            .First(x => x.chars.Distinct().Count() == 14)
            .index + 14;

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
