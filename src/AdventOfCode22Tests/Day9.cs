/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/9
/// </summary>
public class Day9
{
    record Pos(int X, int Y)
    {
        [Flags] public enum Directions { Left = 1, Right = 2, Up = 4, Down = 8 };
        public bool AdjacentTo(Pos other)
        {
            var all = Enumerable.Range(X - 1, 3)
                .SelectMany(x => Enumerable.Range(Y - 1, 3)
                .Select(y => new Pos(x, y)));
            return all.Any(x => x == other);
        }
        public Pos MoveCloserTo(Pos other)
        {
            if (AdjacentTo(other))
            {
                return this;
            }

            if (X == other.X) // moving up or down
            {
                return Move(other.Y > Y ? Directions.Up : Directions.Down);
            }

            if (Y == other.Y) // moving left or roght
            {
                return Move(other.X > X ? Directions.Right : Directions.Left);
            }

            if (X < other.X) // moving to the right in either diagonal
            {
                return Move(Directions.Right | (other.Y > Y ? Directions.Up : Directions.Down));
            }

            if (X > other.X) // moving to the left in either diagonal
            {
                return Move(Directions.Left | (other.Y > Y ? Directions.Up : Directions.Down));
            }

            return this;
        }

        public Pos Move(Directions direction) => direction switch
        {
            Directions.Right | Directions.Up => this with { X = this.X + 1, Y = this.Y + 1 },
            Directions.Right | Directions.Down => this with { X = this.X + 1, Y = this.Y - 1 },
            Directions.Left | Directions.Up => this with { X = this.X - 1, Y = this.Y + 1 },
            Directions.Left | Directions.Down => this with { X = this.X - 1, Y = this.Y - 1 },
            Directions.Left => this with { X = this.X - 1 },
            Directions.Right => this with { X = this.X + 1 },
            Directions.Up => this with { Y = this.Y + 1 },
            Directions.Down => this with { Y = this.Y - 1 },
            _ => this
        };
    }

    [TestCase("""
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-9-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var tailPositions = new HashSet<Pos>();
        var head = new Pos(0, 0);
        var tail = new Pos(0, 0);
        tailPositions.Add(tail);

        input
            .SplitByLines(removeEmptyLines: true, trim: true)
            .SelectMany(x =>
            {
                var l = x.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return Enumerable.Repeat(l[0], int.Parse(l[1]));
            }).ForEach(move =>
            {
                head = head.Move(move switch
                {
                    "R" => Pos.Directions.Right,
                    "L" => Pos.Directions.Left,
                    "U" => Pos.Directions.Up,
                    "D" => Pos.Directions.Down,
                    _ => throw new ArgumentException("Invalid move")
                });
                tail = tail.MoveCloserTo(head);
                tailPositions.Add(tail);
            });

        Console.WriteLine($"Puzzle solution: {tailPositions.Count}");
    }

    [TestCase("""
        R 5
        U 8
        L 8
        D 3
        R 17
        D 10
        L 25
        U 20
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-9-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var tailPositions = new HashSet<Pos>();
        var tails = Enumerable.Repeat(0, 10).Select(_ => new Pos(0, 0)).ToList();
        tailPositions.Add(tails[9]);

        input
            .SplitByLines(removeEmptyLines: true, trim: true)
            .SelectMany(x =>
            {
                var l = x.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return Enumerable.Repeat(l[0], int.Parse(l[1]));
            }).ForEach(move =>
            {
                // head
                tails[0] = tails[0].Move(move switch
                {
                    "R" => Pos.Directions.Right,
                    "L" => Pos.Directions.Left,
                    "U" => Pos.Directions.Up,
                    "D" => Pos.Directions.Down,
                    _ => throw new ArgumentException("Invalid move")
                });
                for (int i = 1; i<10; i++)
                {
                    tails[i] = tails[i].MoveCloserTo(tails[i - 1]);
                }
                tailPositions.Add(tails[9]);
            });

        Console.WriteLine($"Puzzle solution: {tailPositions.Count}");
    }
}
