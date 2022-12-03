namespace AdventOfCode22Tests;

public static class LinqExtensions
{
    /// <summary>
    /// Converts the input string into an integer or returns the <paramref name="defaultValue"/> if the string cannot be converted
    /// </summary>
    /// <param name="input"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int ConvertToInteger(this string input, int defaultValue = default) => int.TryParse(input, out var integer) ? integer : defaultValue;

    /// <summary>
    /// Splits the sequence by lines. Removes empty lines if <paramref name="removeEmptyLines"/> is set. Trims each item if <paramref name="trim"/> is set.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="removeEmptyLines"></param>
    /// <param name="trim"></param>
    /// <returns></returns>
    public static IEnumerable<string> SplitByLines(this string value, bool removeEmptyLines = false, bool trim = false)
    {
        var options = StringSplitOptions.None;
        if (removeEmptyLines)
            options |= StringSplitOptions.RemoveEmptyEntries;
        if (trim)
            options |= StringSplitOptions.TrimEntries;

        var results = value.Split(new[] { "\r\n", "\r", "\n" }, options);

        return results ?? Enumerable.Empty<string>();
    }

    /// <summary>
    /// Chunks the sequence by a <paramref name="splitCondition"/>.
    /// If <paramref name="includeSplitItem"/> is set to true, the item that is the split condition will be included in the yielded sequence.
    /// </summary>
    /// <example>
    /// given 123 456 789 and a split condition ' ' (space), the following will be returned:
    /// when <paramref name="includeSplitItem"/> == <see langword="true"/> => (123 )(345 )(789)
    /// when <paramref name="includeSplitItem"/> == <see langword="false"/> => (123)(345)(789)
    /// </example>
    public static IEnumerable<T[]> ChunkBy<T>(this IEnumerable<T> sequence, Func<T, bool> splitCondition, bool includeSplitItem = false)
    {
        return sequence.ChunkBy((x, _) => splitCondition(x), includeSplitItem);
    }

    /// <summary>
    /// Chunks the sequence by a <paramref name="splitCondition"/>.
    /// If <paramref name="includeSplitItem"/> is set to true, the item that is the split condition will be included in the yielded sequence.
    /// </summary>
    /// <example>
    /// given 123 456 789 and a split condition ' ' (space), the following will be returned:
    /// when <paramref name="includeSplitItem"/> == <see langword="true"/> => (123 )(345 )(789)
    /// when <paramref name="includeSplitItem"/> == <see langword="false"/> => (123)(345)(789)
    /// </example>
    public static IEnumerable<T[]> ChunkBy<T>(this IEnumerable<T> sequence, Func<T, int, bool> splitCondition, bool includeSplitItem = false)
    {
        List<T> items = new();
        var index = 0;
        foreach (var item in sequence)
        {
            if (splitCondition(item, index))
            {
                if (includeSplitItem)
                    items.Add(item);

                yield return items.ToArray();
                items.Clear();
                continue;
            }

            items.Add(item);
        }
    }
}