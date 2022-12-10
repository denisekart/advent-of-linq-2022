/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
using System.Linq;

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/10
/// </summary>
public class Day10
{
    [TestCase("""
        addx 15
        addx -11
        addx 6
        addx -3
        addx 5
        addx -1
        addx -8
        addx 13
        addx 4
        noop
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx -35
        addx 1
        addx 24
        addx -19
        addx 1
        addx 16
        addx -11
        noop
        noop
        addx 21
        addx -15
        noop
        noop
        addx -3
        addx 9
        addx 1
        addx -3
        addx 8
        addx 1
        addx 5
        noop
        noop
        noop
        noop
        noop
        addx -36
        noop
        addx 1
        addx 7
        noop
        noop
        noop
        addx 2
        addx 6
        noop
        noop
        noop
        noop
        noop
        addx 1
        noop
        noop
        addx 7
        addx 1
        noop
        addx -13
        addx 13
        addx 7
        noop
        addx 1
        addx -33
        noop
        noop
        noop
        addx 2
        noop
        noop
        noop
        addx 8
        noop
        addx -1
        addx 2
        addx 1
        noop
        addx 17
        addx -9
        addx 1
        addx 1
        addx -3
        addx 11
        noop
        noop
        addx 1
        noop
        addx 1
        noop
        noop
        addx -13
        addx -19
        addx 1
        addx 3
        addx 26
        addx -30
        addx 12
        addx -1
        addx 3
        addx 1
        noop
        noop
        noop
        addx -9
        addx 18
        addx 1
        addx 2
        noop
        noop
        addx 9
        noop
        noop
        noop
        addx -1
        addx 2
        addx -37
        addx 1
        addx 3
        noop
        addx 15
        addx -21
        addx 22
        addx -6
        addx 1
        noop
        addx 2
        addx 1
        noop
        addx -10
        noop
        noop
        addx 20
        addx 1
        addx 2
        addx 2
        addx -6
        addx -11
        noop
        noop
        noop
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-10-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {

        int? increment = null;
        var value = 1;
        var solution = Enumerable.Repeat("noop", 20).Concat(
            input.SplitByLines(removeEmptyLines: true, trim: true))
            .Select(x => x.Split(" ", StringSplitOptions.TrimEntries))
            .Select(x => (op: x[0], val: x.Length > 1 ? int.Parse(x[1]) : 0))
            .SelectMany(x => x.op == "addx" ? new[] { x, (op: "noop", val: 0) } : new[] { x })
            .Chunk(40)
            .Select(ops =>
            {
                var x = 0;
                ops.ForEach(op =>
                {
                    x = value;
                    if (increment is not null)
                    {
                        value += increment.Value;
                        increment = null;
                    }
                    if (op.op == "addx")
                    {
                        increment = op.val;
                    }
                    // don't care about noop
                });
                return x;
            }).ToList();
        var other = solution
            .Select((val, idx) => (v: val * (((idx + 1) * 40) - 20), i: (((idx + 1) * 40) - 20)))
            .Where(x => new[] { 20, 60, 100, 140, 180, 220 }.Contains(x.i))
            .Select(x => x.v)
            .Sum();

        Console.WriteLine($"Puzzle solution: {other}");
    }

    [TestCase("""
        addx 15
        addx -11
        addx 6
        addx -3
        addx 5
        addx -1
        addx -8
        addx 13
        addx 4
        noop
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx -35
        addx 1
        addx 24
        addx -19
        addx 1
        addx 16
        addx -11
        noop
        noop
        addx 21
        addx -15
        noop
        noop
        addx -3
        addx 9
        addx 1
        addx -3
        addx 8
        addx 1
        addx 5
        noop
        noop
        noop
        noop
        noop
        addx -36
        noop
        addx 1
        addx 7
        noop
        noop
        noop
        addx 2
        addx 6
        noop
        noop
        noop
        noop
        noop
        addx 1
        noop
        noop
        addx 7
        addx 1
        noop
        addx -13
        addx 13
        addx 7
        noop
        addx 1
        addx -33
        noop
        noop
        noop
        addx 2
        noop
        noop
        noop
        addx 8
        noop
        addx -1
        addx 2
        addx 1
        noop
        addx 17
        addx -9
        addx 1
        addx 1
        addx -3
        addx 11
        noop
        noop
        addx 1
        noop
        addx 1
        noop
        noop
        addx -13
        addx -19
        addx 1
        addx 3
        addx 26
        addx -30
        addx 12
        addx -1
        addx 3
        addx 1
        noop
        noop
        noop
        addx -9
        addx 18
        addx 1
        addx 2
        noop
        noop
        addx 9
        noop
        noop
        noop
        addx -1
        addx 2
        addx -37
        addx 1
        addx 3
        noop
        addx 15
        addx -21
        addx 22
        addx -6
        addx 1
        noop
        addx 2
        addx 1
        noop
        addx -10
        noop
        noop
        addx 20
        addx 1
        addx 2
        addx 2
        addx -6
        addx -11
        noop
        noop
        noop
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-10-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        int? increment = null;
        var value = 1;
        var row = "";

        var solution =
            input.SplitByLines(removeEmptyLines: true, trim: true)
            .Select(x => x.Split(" ", StringSplitOptions.TrimEntries))
            .Select(x => (op: x[0], val: x.Length > 1 ? int.Parse(x[1]) : 0))
            .SelectMany(x => x.op == "addx" ? new[] { x, (op: "noop", val: 0) } : new[] { x })
            .Chunk(40)
            .Select(ops =>
            {
                var x = 0; // pixel
                // value == sprite
                ops.ForEach(op =>
                {
                    // check if sprite is at position
                    if (Math.Abs(value - x) < 2 || Math.Abs(value + x) < 2)
                    {
                        row += "#";
                    }
                    else
                    {
                        row += ".";
                    }

                    // exec operation
                    //x = value;
                    if (increment is not null)
                    {
                        value += increment.Value;
                        increment = null;
                    }
                    if (op.op == "addx")
                    {
                        increment = op.val;
                    }
                    // don't care about noop
                    x++;
                });
                Console.WriteLine(row);
                row = "";
                return value;
            }).ToList();
    }
}
