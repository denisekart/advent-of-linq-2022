/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/13
/// </summary>
public class Day13
{
    int Compare(JsonNode? left, JsonNode? right)
    {
        if (left is JsonValue jvl && right is JsonValue jvr)
        {
            // were using thit in the list.Sort (so negative, zero or positive)
            return jvl.GetValue<int>() - jvr.GetValue<int>();
        }
        // normalize to array
        var lArray = left is JsonValue lv ? new JsonArray(lv.GetValue<int>()) : left as JsonArray;
        var rArray = right is JsonValue rv ? new JsonArray(rv.GetValue<int>()) : right as JsonArray;

        return Enumerable.Zip(lArray, rArray)
            .Select(p => Compare(p.First, p.Second))
            // either we were not "undetermined" - comparrison failed or it passed
            // or we compare lengths of arrays because right needs to be same or longer
            .FirstOrDefault(c => c != 0, lArray.Count - rArray.Count);
    }

    [TestCase("""
        [1,1,3,1,1]
        [1,1,5,1,1]

        [[1],[2,3,4]]
        [[1],4]

        [9]
        [[8,7,6]]

        [[4,4],4,4]
        [[4,4],4,4,4]

        [7,7,7,7]
        [7,7,7]

        []
        [3]

        [[[]]]
        [[]]

        [1,[2,[3,[4,[5,6,7]]]],8,9]
        [1,[2,[3,[4,[5,6,0]]]],8,9]
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-13-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        bool AreInOrder(string left, string right)
        {
            var l = JsonNode.Parse(left);
            var r = JsonNode.Parse(right);
            // either undetermined (probably never happens) or correct
            return Compare(l, r) <= 0;
        }

        var solution = Regex.Split(input, @"\n\s*\n\s*")
            .Select(x => x.SplitByLines(removeEmptyLines: true, trim: true).ToArray())
            .Select((x, i) => (left: x[0], right: x[1], index: i + 1))
            .Select(x => (x.index, inOrder: AreInOrder(x.left, x.right)))
            .Where(x => x.inOrder)
            .ToList();
        Console.WriteLine($"In order: {string.Join(",", solution.Select(x => x.index))}");
        Console.WriteLine($"Puzzle solution: {solution.Sum(x => x.index)}");
    }

    [TestCase("""
        [1,1,3,1,1]
        [1,1,5,1,1]
        
        [[1],[2,3,4]]
        [[1],4]
        
        [9]
        [[8,7,6]]
        
        [[4,4],4,4]
        [[4,4],4,4,4]
        
        [7,7,7,7]
        [7,7,7]
        
        []
        [3]
        
        [[[]]]
        [[]]
        
        [1,[2,[3,[4,[5,6,7]]]],8,9]
        [1,[2,[3,[4,[5,6,0]]]],8,9]
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-13-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {
        var all = $"""
            [[2]]
            [[6]]
            {input}
            """
            .SplitByLines(removeEmptyLines: true, trim: true)
            .Select(x => JsonNode.Parse(x))
            .ToList();

        // we need these later
        var dividers = new[] { all[0], all[1] };
        all.Sort(Compare);

        var first = all.IndexOf(dividers[0]) + 1; // 1 based
        var last = all.IndexOf(dividers[1]) + 1;

        Console.WriteLine($"Puzzle solution: {first * last}");
    }
}
