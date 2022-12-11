/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/11
/// </summary>
public partial class Day11
{
    class Monkey
    {
        public long InspectionCount { get; set; }
        public Queue<long> Items { get; set; }
        public Func<long, long> Operation { get; set; }
        public Func<long, bool> Test { get; set; }
        public Dictionary<bool, int> Decisions { get; set; }
        public long DivisableBy { get; internal set; }
    }

    [TestCase("""
        Monkey 0:
          Starting items: 79, 98
          Operation: new = old * 19
          Test: divisible by 23
            If true: throw to monkey 2
            If false: throw to monkey 3

        Monkey 1:
          Starting items: 54, 65, 75, 74
          Operation: new = old + 6
          Test: divisible by 19
            If true: throw to monkey 2
            If false: throw to monkey 0

        Monkey 2:
          Starting items: 79, 60, 97
          Operation: new = old * old
          Test: divisible by 13
            If true: throw to monkey 1
            If false: throw to monkey 3

        Monkey 3:
          Starting items: 74
          Operation: new = old + 3
          Test: divisible by 17
            If true: throw to monkey 0
            If false: throw to monkey 1
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-11-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var monkeys = Regex.Split(input, @"\n\s*\n\s*")
            .Select(m =>
            {
                var lines = m.SplitByLines(removeEmptyLines: true, trim: true).ToList();
                var monkey = new Monkey();
                monkey.Items = new Queue<long>(lines[1].Substring("Starting items: ".Length)
                .Split(",", StringSplitOptions.TrimEntries)
                .Select(long.Parse));
                var op = lines[2].Substring("Operation: new = ".Length).Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                monkey.Operation = (old) =>
                {
                    var l = op[0] == "old" ? old : long.Parse(op[0]);
                    var r = op[2] == "old" ? old : long.Parse(op[2]);

                    return op[1] switch
                    {
                        "+" => l + r,
                        "-" => l - r,
                        "*" => l * r,
                        "/" => l / r,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                };
                var divisableBy = long.Parse(lines[3].Substring("Test: divisible by ".Length));
                monkey.Test = value => (value % divisableBy) == 0;

                var trueCondition = int.Parse(lines[4].Substring("If true: throw to monkey ".Length));
                var falseCondition = int.Parse(lines[5].Substring("If false: throw to monkey ".Length));
                monkey.Decisions = new()
                {
                    {true, trueCondition },
                    {false, falseCondition}
                };

                return monkey;
            }).ToList();

        Enumerable.Range(0, 20)
            .ForEach(round =>
            {
                monkeys.ForEach(monkey =>
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        var op = monkey.Operation(item) / 3;
                        var condition = monkey.Test(op);
                        var toMonkey = monkey.Decisions[condition];
                        monkeys[toMonkey].Items.Enqueue(op);
                        monkey.InspectionCount++;
                    }
                });
            });

        var monkeyBusiness = monkeys
            .OrderByDescending(x => x.InspectionCount)
            .Take(2)
            .Select(x => x.InspectionCount)
            .Aggregate((x, a) => a * x);


        Console.WriteLine($"Puzzle solution: {monkeyBusiness}");
    }

    [TestCase("""
        Monkey 0:
          Starting items: 79, 98
          Operation: new = old * 19
          Test: divisible by 23
            If true: throw to monkey 2
            If false: throw to monkey 3

        Monkey 1:
          Starting items: 54, 65, 75, 74
          Operation: new = old + 6
          Test: divisible by 19
            If true: throw to monkey 2
            If false: throw to monkey 0

        Monkey 2:
          Starting items: 79, 60, 97
          Operation: new = old * old
          Test: divisible by 13
            If true: throw to monkey 1
            If false: throw to monkey 3

        Monkey 3:
          Starting items: 74
          Operation: new = old + 3
          Test: divisible by 17
            If true: throw to monkey 0
            If false: throw to monkey 1
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-11-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        List<Monkey> monkeys = null;
        monkeys = Regex.Split(input, @"\n\s*\n\s*")
            .Select(m =>
            {
                var lines = m.SplitByLines(removeEmptyLines: true, trim: true).ToList();
                var monkey = new Monkey();
                monkey.Items = new Queue<long>(lines[1].Substring("Starting items: ".Length)
                .Split(",", StringSplitOptions.TrimEntries)
                .Select(long.Parse));
                var op = lines[2].Substring("Operation: new = ".Length).Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                monkey.Operation = (old) =>
                {
                    var l = op[0] == "old" ? old : long.Parse(op[0]);
                    var r = op[2] == "old" ? old : long.Parse(op[2]);
                    var m = monkeys.Select(x => x.DivisableBy).Aggregate((x, y) => x * y);

                    return op[1] switch
                    {
                        "+" => l + r,
                        "-" => l - r,
                        "*" => checked(l * r) % m,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                };
                var divisableBy = long.Parse(lines[3].Substring("Test: divisible by ".Length));
                monkey.DivisableBy = divisableBy;
                monkey.Test = value => (value % divisableBy) == 0;

                var trueCondition = int.Parse(lines[4].Substring("If true: throw to monkey ".Length));
                var falseCondition = int.Parse(lines[5].Substring("If false: throw to monkey ".Length));
                monkey.Decisions = new()
                {
                    {true, trueCondition },
                    {false, falseCondition}
                };

                return monkey;
            }).ToList();

        Enumerable.Range(1, 10000)
            .ForEach(round =>
            {
                monkeys.ForEach(monkey =>
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        var op = monkey.Operation(item);
                        var condition = monkey.Test(op);
                        var toMonkey = monkey.Decisions[condition];
                        monkeys[toMonkey].Items.Enqueue(op);
                        monkey.InspectionCount++;
                    }
                });
            });

        var monkeyBusiness = monkeys
            .OrderByDescending(x => x.InspectionCount)
            .Take(2)
            .Select(x => x.InspectionCount)
            .Aggregate((x, a) => a * x);


        Console.WriteLine($"Puzzle solution: {monkeyBusiness}");
    }
}
