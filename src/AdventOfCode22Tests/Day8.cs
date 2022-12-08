/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
using System.Threading.Tasks.Sources;

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/8
/// </summary>
public class Day8
{
    bool Left(int index, string line) => index == 0 || line.Take(index).Select(x => (int)x).Max() < (int)line[index];
    bool Right(int index, string line) => index == line.Length - 1 || line.Skip(index + 1).Select(x => (int)x).Max() < (int)line[index];
    bool Up(int line, int index, string[] lines) => line == 0 || lines.Take(line).Select(c => (int)c.Skip(index).First()).Max() < (char)lines[line][index];
    bool Down(int line, int index, string[] lines) => line == lines.Length - 1 || lines.Skip(line + 1).Select(c => (int)c.Skip(index).First()).Max() < (char)lines[line][index];

    [TestCase("""
        30373
        25512
        65332
        33549
        35390
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-8-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var allLines = input.SplitByLines(removeEmptyLines: true, trim: true).ToArray();
        var height = allLines.Count();
        var width = allLines[0].Count();

        var solution = Enumerable.Range(0, height)
            .SelectMany(h => Enumerable.Range(0, width)
                .Select(w => (h, w)))
            .Select(item =>
            {
                var isVisible = (item.h == 0 || item.w == 0 || item.h == height - 1 || item.w == width - 1) || (
                                Left(item.w, allLines[item.h])
                                || Right(item.w, allLines[item.h])
                                || Up(item.h, item.w, allLines)
                                || Down(item.h, item.w, allLines)
                            );
                return (item, isVisible);
            })
            .Where(x => x.isVisible)
            .Count();

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    int DistLeft(int index, string line) => index == 0 ? 0 : (line.Take(index).Select(x => (int)x).Reverse().TakeWhile(x => x < (int)line[index]).Count());
    int DistRight(int index, string line) => index == line.Length - 1 ? 0 : (line.Skip(index + 1).Select(x => (int)x).TakeWhile(x => x < (int)line[index]).Count());
    int DistUp(int line, int index, string[] lines) => line == 0 ? 0 : (lines.Take(line).Reverse().Select(c => (int)c.Skip(index).First()).TakeWhile(x => x <(int)lines[line][index]).Count());
    int DistDown(int line, int index, string[] lines) => line == lines.Length - 1 ? 0 : (lines.Skip(line + 1).Select(c => (int)c.Skip(index).First()).TakeWhile(x => x < (int)lines[line][index]).Count());

    [TestCase("""
        30373
        25512
        65332
        33549
        35390
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-8-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var allLines = input.SplitByLines(removeEmptyLines: true, trim: true).ToArray();
        var height = allLines.Count();
        var width = allLines[0].Count();

        var solution = Enumerable.Range(0, height)
            .SelectMany(h => Enumerable.Range(0, width)
                .Select(w => (h, w)))
            .Select(item =>
            {
                var l = DistLeft(item.w, allLines[item.h]);
                l += (Left(item.w, allLines[item.h]) ? 0 : 1);
                var r = DistRight(item.w, allLines[item.h]);
                r += (Right(item.w, allLines[item.h]) ? 0 : 1);
                var u = DistUp(item.h, item.w, allLines);
                u += (Up(item.h, item.w, allLines) ? 0 : 1);
                var d = DistDown(item.h, item.w, allLines);
                d += (Down(item.h, item.w, allLines) ? 0 : 1);

                return (item, score: (l * r * u * d));
            })
            .Max(x => x.score);

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
