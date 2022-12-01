using System.Text.RegularExpressions;

namespace AdventOfCode22Tests;

public partial class Day1
{
    /// <summary>
    /// Just to be absolutely clear - this is an overkill!
    /// </summary>
    /// <returns>A generated regular expression that will match at lease two consecutive newline characters with no significant characters in between</returns>
    [GeneratedRegex("\\n\\s*\\n\\s*", RegexOptions.CultureInvariant)]
    private static partial Regex TwoConsecutiveNewLines();

    [GeneratedRegex("\\n\\s*", RegexOptions.CultureInvariant)]
    private static partial Regex NewLine();

    [TestCase("""
        1000
        2000
        3000

        4000

        5000
        6000

        7000
        8000
        9000

        10000
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-1-data.txt" })]
    public void Part1_ShouldReturnGroupWithMaxCaloriesWhenGivenAValidInputString(string input)
    {
        var maxCalories = 
            TwoConsecutiveNewLines().Split(input)
            .Select(group => NewLine().Split(group))
            .Select(group => group.Select(item => item.ConvertToInteger()))
            .Select(group => group.Sum())
            .Max();

        Console.WriteLine($"Puzzle solution: {maxCalories}");
    }

    [TestCase("""
        1000
        2000
        3000

        4000

        5000
        6000

        7000
        8000
        9000

        10000
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-1-data.txt" })]
    public void Part2_ShouldReturnSumOfTop3GroupsWithMostCaloriesWhenGivenAValidInputString(string input)
    {
        var maxCalories =
            TwoConsecutiveNewLines().Split(input)
            .Select(group => NewLine().Split(group))
            .Select(group => group.Select(item => item.ConvertToInteger()))
            .Select(group => group.Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        Console.WriteLine($"Puzzle solution: {maxCalories}");
    }
}
