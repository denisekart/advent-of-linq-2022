/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!
namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/5
/// </summary>
public class Day5
{
    Stack<string>[] GetConfiguration(string[] input)
    {
        var columnCount = input[input.Length - 1]
            .Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Length;

        var configurations = new Stack<string>[columnCount];
        input
            .Reverse()
            .Skip(1)
            .ForEach(line =>
            {
                var columns = line
                    .Chunk(4)
                    .Select(x => new string(x).Trim())
                    .ToArray();
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (!string.IsNullOrWhiteSpace(columns[i]))
                    {
                        configurations[i] ??= new();
                        configurations[i].Push(columns[i].Substring(1, columns[i].Length - 2));
                    }
                }
            });
        return configurations;
    }

    List<(int count, int fromCol, int toCol)> GetMoves(string[] input)
    {
        return input
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Select(x => (
                count: int.Parse(x[1]),
                fromCol: int.Parse(x[3]) - 1,
                toCol: int.Parse(x[5]) - 1))
            .ToList();
    }

    [TestCase("""
            [D]    
        [N] [C]    
        [Z] [M] [P]
         1   2   3 

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-5-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        var chunks = input
            .SplitByLines(trim: false, removeEmptyLines: false)
            .ChunkBy(string.IsNullOrWhiteSpace)
            .ToArray();
        var configuration = GetConfiguration(chunks.First().ToArray());
        var moves = GetMoves(chunks.Last().ToArray());
        moves
            .ForEach(move =>
                Enumerable.Repeat(0, move.count)
                .ForEach(_ =>
                {
                    var item = configuration[move.fromCol].Pop();
                    configuration[move.toCol].Push(item);
                }));

        var solution = string.Join("", configuration
            .Select(x => x.TryPop(out var r) ? r : string.Empty))
            .Replace("[", "")
            .Replace("]", "");

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
            [D]    
        [N] [C]    
        [Z] [M] [P]
         1   2   3 

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-5-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var chunks = input
            .SplitByLines(trim: false, removeEmptyLines: false)
            .ChunkBy(string.IsNullOrWhiteSpace)
            .ToArray();
        var configuration = GetConfiguration(chunks.First().ToArray());
        var moves = GetMoves(chunks.Last().ToArray());
        moves.ForEach(move =>
            Enumerable.Repeat(0, move.count)
                .Select(_ => configuration[move.fromCol].Pop())
                .Reverse()
                .ForEach(pop => configuration[move.toCol].Push(pop)));
        var solution = string.Join("", configuration
            .Select(x => x.TryPop(out var r) ? r : string.Empty))
            .Replace("[", "")
            .Replace("]", "");

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
