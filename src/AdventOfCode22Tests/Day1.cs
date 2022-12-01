namespace AdventOfCode22Tests;

public class Day1
{
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
        """, ExpectedResult = 24000)]
    public int Part1_ShouldReturnGroupWithMaxCaloriesWhenGivenAValidInputString(string input)
    {
        var maxCalories = input
            .Split($"{Environment.NewLine}{Environment.NewLine}")
            .Select(group => group.Split(Environment.NewLine))
            .Select(group => group.Select(item => item.ConvertToInteger()))
            .Select(group => group.Sum())
            .Max();

        Console.WriteLine($"Puzzle solution: {maxCalories}");

        return maxCalories;
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
        """, ExpectedResult = 45000)]
    public int Part2_ShouldReturnSumOfTop3GroupsWithMostCaloriesWhenGivenAValidInputString(string input)
    {
        var maxCalories = input
            .Split($"{Environment.NewLine}{Environment.NewLine}")
            .Select(group => group.Split(Environment.NewLine))
            .Select(group => group.Select(item => item.ConvertToInteger()))
            .Select(group => group.Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        Console.WriteLine($"Puzzle solution: {maxCalories}");

        return maxCalories;
    }
}
