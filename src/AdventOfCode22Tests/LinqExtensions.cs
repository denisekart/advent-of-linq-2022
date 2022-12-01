namespace AdventOfCode22Tests;

public static class LinqExtensions
{
    public static int ConvertToInteger(this string input, int defaultValue = default) => int.TryParse(input, out var integer) ? integer : defaultValue;
}