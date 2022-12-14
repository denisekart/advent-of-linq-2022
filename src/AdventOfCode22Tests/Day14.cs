/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/14
/// </summary>
public class Day14
{
    [TestCase("""
        498,4 -> 498,6 -> 496,6
        503,4 -> 502,4 -> 502,9 -> 494,9
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-14-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var map = new Dictionary<(int x, int y), char>();

        input.SplitByLines(removeEmptyLines: true, trim: true).ForEach(line =>
        {
            line.Split(" -> ")
                .Select(x => x.Split(','))
                .Select(s => (x: int.Parse(s[0]), y: int.Parse(s[1])))
                .Window()
                .ForEach(tuple =>
                {
                    if (tuple.Item1.x == tuple.Item2.x) // vertical
                    {
                        var min = int.Min(tuple.Item1.y, tuple.Item2.y);
                        var max = int.Max(tuple.Item1.y, tuple.Item2.y);
                        Enumerable.Range(min, max - min + 1).ForEach(y => map[(tuple.Item1.x, y)] = '#');
                    }
                    else // horizontal
                    {
                        var min = int.Min(tuple.Item1.x, tuple.Item2.x);
                        var max = int.Max(tuple.Item1.x, tuple.Item2.x);
                        Enumerable.Range(min, max - min + 1).ForEach(x => map[(x, tuple.Item1.y)] = '#');
                    }
                });
        });

        var yMax = map.Keys.Select(x => x.y).Max();
        var grains = 0;
        while (true)
        {
            var current = (x: 500, y: 0);
            var done = false;
            while (true)
            {

                var pos = current with { y = current.y + 1 }; // straight down
                if (pos.y > yMax) // overflow down (the only way) - nothing left to do, this is the solution
                {
                    done = true;
                    break;
                }
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                pos = current with { x = current.x - 1, y = current.y + 1 }; // left
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                pos = current with { x = current.x + 1, y = current.y + 1 }; // roght
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                break; // can't move
            }

            if (done)
            {
                break;
            }
            map[current] = 'o'; // at this point, I really don't need a distinction between sand and rock but hey
            grains++; // brains, lol
        }

        Console.WriteLine($"Puzzle solution: {grains}");
    }

    [TestCase("""
        498,4 -> 498,6 -> 496,6
        503,4 -> 502,4 -> 502,9 -> 494,9
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-14-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var map = new Dictionary<(int x, int y), char>();

        input.SplitByLines(removeEmptyLines: true, trim: true).ForEach(line =>
        {
            line.Split(" -> ")
                .Select(x => x.Split(','))
                .Select(s => (x: int.Parse(s[0]), y: int.Parse(s[1])))
                .Window()
                .ForEach(tuple =>
                {
                    if (tuple.Item1.x == tuple.Item2.x) // vertical
                    {
                        var min = int.Min(tuple.Item1.y, tuple.Item2.y);
                        var max = int.Max(tuple.Item1.y, tuple.Item2.y);
                        Enumerable.Range(min, max - min + 1).ForEach(y => map[(tuple.Item1.x, y)] = '#');
                    }
                    else // horizontal
                    {
                        var min = int.Min(tuple.Item1.x, tuple.Item2.x);
                        var max = int.Max(tuple.Item1.x, tuple.Item2.x);
                        Enumerable.Range(min, max - min + 1).ForEach(x => map[(x, tuple.Item1.y)] = '#');
                    }
                });
        });

        var yMax = map.Keys.Select(x => x.y).Max() + 2; // this is where I draw the line! - lol (I hope none of my clients see these **itty comments)
        var xMin = map.Keys.Select(x => x.x).Min();
        var xMax = map.Keys.Select(x => x.x).Max();
        // this is probably the max amount that the grains can spill - probably
        // I figured this out way later, but I could have just stopped the grains "mid-air" later when running the simulation - ahh well
        Enumerable.Range(xMin - yMax, xMax - xMin + yMax + yMax).ForEach(x => map[(x, yMax)] = '#');


        var grains = 0;
        while (true)
        {
            var current = (x: 500, y: 0);
            if (map.ContainsKey(current))
                break; // we can't fill up anything more - this could have gone sideways in part 1

            while (true)
            {
                var pos = current with { y = current.y + 1 }; // straight down
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                pos = current with { x = current.x - 1, y = current.y + 1 }; // left
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                pos = current with { x = current.x + 1, y = current.y + 1 }; // roght
                if (!map.ContainsKey(pos))
                {
                    current = pos;
                    continue; // done for this iteration
                }

                break; // can't move
            }
            map[current] = 'o'; // at this point, I really don't need a distinction between sand and rock but hey
            grains++; // brains, lol
        }

        Console.WriteLine($"Puzzle solution: {grains}");
    }
}
