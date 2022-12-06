/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/4
/// </summary>
public class Day4
{
    int[] Expand(string range)
    {
        var segments = range.Split("-", StringSplitOptions.TrimEntries);
        var start = int.Parse(segments[0]);
        var end = int.Parse(segments[1]);
        var len = end - start + 1;
        return Enumerable.Range(start, len).ToArray();
    }

    [TestCase("""
        2-4,6-8
        2-3,4-5
        5-7,7-9
        2-8,3-7
        6-6,4-6
        2-6,4-8
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-4-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var solution = input
            .SplitByLines(trim: true, removeEmptyLines: true)
            .Select(l => l.Split(",", StringSplitOptions.TrimEntries))
            .Select(l => (left: Expand(l[0]), right: Expand(l[1])))
            .Select(x => (x.left, x.right, intersection: x.left.Intersect(x.right).ToArray()))
            .Where(x => x.intersection.Length == x.left.Length || x.intersection.Length == x.right.Length)
            .Count();

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        2-4,6-8
        2-3,4-5
        5-7,7-9
        2-8,3-7
        6-6,4-6
        2-6,4-8
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-4-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var solution = input
            .SplitByLines(trim: true, removeEmptyLines: true)
            .Select(l => l.Split(",", StringSplitOptions.TrimEntries))
            .Select(l => (left: Expand(l[0]), right: Expand(l[1])))
            .Select(x => (x.left, x.right, intersection: x.left.Intersect(x.right).ToArray()))
            .Where(x => x.intersection.Length > 0)
            .Count();

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
