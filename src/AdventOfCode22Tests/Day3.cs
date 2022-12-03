
using System.Text.RegularExpressions;
/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/3
/// </summary>
public  class Day3
{
    // Lowercase item types a through z have priorities 1 through 26.
    // Uppercase item types A through Z have priorities 27 through 52.
    int Prioritize(char item)
    {
        if (char.IsUpper(item))
        {
            return ((int)item) - 38;
        }
        return ((int)item) - 96;
    }

    [TestCase("""
        vJrwpWtwJgWrhcsFMMfFFhFp
        jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
        PmmdzqPrVvPwwTWBwg
        wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
        ttgJtRGJQctTZtZT
        CrZsJsPPZsGzwwsLwLmpwMDw
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-3-data.txt"})]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var solution = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.TrimEnd())
            .Select(rucksack => (
                left: rucksack.Take(rucksack.Length/2), 
                right: rucksack.Skip(rucksack.Length / 2)))
            .Select(r => (r.left, r.right, inBoth: r.left.Intersect(r.right)))
            .Select(r => r.inBoth.Distinct())
            .Select(x => x.Select(Prioritize))
            .Sum(x => x.Sum());

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        vJrwpWtwJgWrhcsFMMfFFhFp
        jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
        PmmdzqPrVvPwwTWBwg
        wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
        ttgJtRGJQctTZtZT
        CrZsJsPPZsGzwwsLwLmpwMDw
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-3-data.txt"})]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var solution = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.TrimEnd())
            .Chunk(3)
            .Select(c => c[0].Intersect(c[1]).Intersect(c[2]))
            .Select(c => c.Distinct())
            .Select(x => x.Select(Prioritize))
            .Sum(x => x.Sum());

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
