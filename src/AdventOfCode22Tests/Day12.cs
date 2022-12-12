/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/12
/// </summary>
public class Day12
{
    [TestCase("""
        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-12-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var allLines = input
            .SplitByLines(removeEmptyLines: true, trim: true)
            .ToArray();
        var height = allLines.Length;
        var width = allLines[0].Length;
        var matrix = allLines
            .SelectMany(x => x.AsEnumerable())
            .ToList();
        var start = matrix.FindIndex(x => x == 'S');
        var goal = matrix.FindIndex(x => x == 'E');
        matrix[start] = 'a';
        matrix[goal] = 'z';

        var stack = new Queue<(int pos, int steps)>(); // yes, I know it's not a stack!
        HashSet<int> seen = new HashSet<int>();
        stack.Enqueue((start, 0));
        var solution = int.MaxValue;

        while (stack.TryDequeue(out var pos))
        {
            if (pos.pos == goal)
            {
                solution = pos.steps;
                break;
            }

            new[] {
                ((pos.pos + 1) % width) < pos.pos % width ? pos.pos : pos.pos + 1, // right
                ((pos.pos - 1) % width) > pos.pos % width ? pos.pos : pos.pos - 1, // left
                pos.pos + width, // down
                pos.pos - width // up
            }
            // in the grid
            .Where(p => p >= 0 && p < height * width && p != pos.pos)
            // not higher than 1
            .Where(p => ((int)matrix[p]) <= ((int)matrix[pos.pos] + 1))
            // only non recursive
            .Where(p => seen.Add(p))
            .ForEach(d =>
            {
                stack.Enqueue((
                    pos: d,
                    steps: pos.steps + 1)
                    );
            });

        }

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-12-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var allLines = input
            .SplitByLines(removeEmptyLines: true, trim: true)
            .ToArray();
        var height = allLines.Length;
        var width = allLines[0].Length;
        var matrix = allLines
            .SelectMany(x => x.AsEnumerable())
            .ToList();
        var start = matrix.FindIndex(x => x == 'S');
        var goal = matrix.FindIndex(x => x == 'E');
        matrix[start] = 'a';
        matrix[goal] = 'z';

        var stack = new Queue<(int pos, int steps)>();
        HashSet<int> seen = new HashSet<int>();

        var solution = int.MaxValue;
        foreach (var index in matrix
            .Select((x, i) => (x, i))
            .Where(x => x.x == 'a')
            .Select(x => x.i))
        {
            stack.Clear();
            seen.Clear();
            stack.Enqueue((index, 0));

            while (stack.TryDequeue(out var pos))
            {
                if (pos.pos == goal)
                {
                    if (solution > pos.steps)
                        solution = pos.steps;
                    break;
                }

                new[] {
                ((pos.pos + 1) % width) < pos.pos % width ? pos.pos : pos.pos + 1, // right
                ((pos.pos - 1) % width) > pos.pos % width ? pos.pos : pos.pos - 1, // left
                pos.pos + width, // down
                pos.pos - width // up
            }
                // in the grid
                .Where(p => p >= 0 && p < height * width && p != pos.pos)
                // not higher than 1
                .Where(p => ((int)matrix[p]) <= ((int)matrix[pos.pos] + 1))
                // only non recursive
                .Where(p => seen.Add(p))
                .ForEach(d =>
                {
                    stack.Enqueue((
                        pos: d,
                        steps: pos.steps + 1)
                        );
                });

            }
        }
        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
