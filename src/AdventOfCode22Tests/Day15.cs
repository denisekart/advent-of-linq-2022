/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/15
/// </summary>
public class Day15
{
    (int x, int y) Coord(string txt)
    {
        var coords = txt.Split(", ");
        return (int.Parse(coords[0].Substring(2)), int.Parse(coords[1].Substring(2)));
    }

    [TestCase("""
        Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        Sensor at x=9, y=16: closest beacon is at x=10, y=16
        Sensor at x=13, y=2: closest beacon is at x=15, y=3
        Sensor at x=12, y=14: closest beacon is at x=10, y=16
        Sensor at x=10, y=20: closest beacon is at x=10, y=16
        Sensor at x=14, y=17: closest beacon is at x=10, y=16
        Sensor at x=8, y=7: closest beacon is at x=2, y=10
        Sensor at x=2, y=0: closest beacon is at x=2, y=10
        Sensor at x=0, y=11: closest beacon is at x=2, y=10
        Sensor at x=20, y=14: closest beacon is at x=25, y=17
        Sensor at x=17, y=20: closest beacon is at x=21, y=22
        Sensor at x=16, y=7: closest beacon is at x=15, y=3
        Sensor at x=14, y=3: closest beacon is at x=15, y=3
        Sensor at x=20, y=1: closest beacon is at x=15, y=3
        """)]
    //[TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-15-data.txt"})]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var solution = input.SplitByLines(trim: true, removeEmptyLines: true)
            .Select(x => x.Split(": "))
            .Select(x => (
                sensor: x[0].Substring("Sensor at ".Length), 
                beacon: x[1].Substring("closest beacon is at ".Length)))
            .Select(x => (sensor: Coord(x.sensor), beacon: Coord(x.beacon)));

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        Sensor at x=9, y=16: closest beacon is at x=10, y=16
        Sensor at x=13, y=2: closest beacon is at x=15, y=3
        Sensor at x=12, y=14: closest beacon is at x=10, y=16
        Sensor at x=10, y=20: closest beacon is at x=10, y=16
        Sensor at x=14, y=17: closest beacon is at x=10, y=16
        Sensor at x=8, y=7: closest beacon is at x=2, y=10
        Sensor at x=2, y=0: closest beacon is at x=2, y=10
        Sensor at x=0, y=11: closest beacon is at x=2, y=10
        Sensor at x=20, y=14: closest beacon is at x=25, y=17
        Sensor at x=17, y=20: closest beacon is at x=21, y=22
        Sensor at x=16, y=7: closest beacon is at x=15, y=3
        Sensor at x=14, y=3: closest beacon is at x=15, y=3
        Sensor at x=20, y=1: closest beacon is at x=15, y=3
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData),new object[] {"day-15-data.txt"})]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var solution = 0;

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
