/// This file was generated automatically by a tool.
/// Test data was automatically downloaded from https://adventofcode.com/
/// Really! I'm not joking, it's awesome! Try it out!
/// dotnet run --project .\tools -- --help
/// 
/// Happy coding!

namespace AdventOfCode22Tests;

/// <summary>
/// The problem: https://adventofcode.com/2022/day/2
/// </summary>
public partial class Day2
{
    int WinScore((int myScore, bool isDraw, bool victoryForMe, bool victoryForOponent) round) =>
            (round.isDraw ? 3 : round.victoryForMe ? 6 : 0);
    bool IsVictoryForA(int a, int b) => (b % 3) == (a + 2) % 3;
    int ConvertToScore(string input) => input switch
    {
        "A" => 1,
        "B" => 2,
        "C" => 3,
    };
    bool VictoryMatrixForA(int a, int b) => a switch
    {
        1 when b == 3 => true,
        2 when b == 1 => true,
        3 when b == 2 => true,
        _ => false
    };

    [TestCase("""
        A Y
        B X
        C Z
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-2-data.txt" })]
    public void Part1_ShouldReturnValidSolution(string input)
    {
        string StrategyForMe(string input) => input switch
        {
            "X" => "A",
            "Y" => "B",
            "Z" => "C",
        };

        var solution = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(round => round.Split(" "))
            .Select(round => (
                oponent: round[0]?.Trim(),
                me: round[1]?.Trim()))
            .Select(round => (
                oponent: round.oponent,
                me: StrategyForMe(round.me)))
            .Select(round => (
                oponent: ConvertToScore(round.oponent),
                me: ConvertToScore(round.me)))
            .Select(round => (
                myScore: round.me,
                isDraw: round.oponent == round.me,
                victoryForMe: IsVictoryForA(round.me, round.oponent),
                victoryForOponent: IsVictoryForA(round.oponent, round.me)))
            .Select(round => (
                myScore: round.myScore,
                winScore: WinScore(round)))
            .Select(round => round.myScore + round.winScore)
            .Sum();

        Console.WriteLine($"Puzzle solution: {solution}");
    }

    [TestCase("""
        A Y
        B X
        C Z
        """)]
    [TestCaseSource(typeof(Utilities), nameof(Utilities.LoadTestData), new object[] { "day-2-data.txt" })]
    public void Part2_ShouldReturnValidSolution(string input)
    {


        string StrategyForMe(string me, string oponent) => oponent switch
        {
            _ when me == "Y" => oponent,
            "A" when me == "Z" => "B",
            "B" when me == "Z" => "C",
            "C" when me == "Z" => "A",
            "A" when me == "X" => "C",
            "B" when me == "X" => "A",
            "C" when me == "X" => "B",
        };

        var solution = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(round => round.Split(" "))
            .Select(round => (
                oponent: round[0]?.Trim(),
                me: round[1]?.Trim()))
            .Select(round => (
                oponent: round.oponent,
                me: StrategyForMe(round.me, round.oponent)))
            .Select(round => (
                oponent: ConvertToScore(round.oponent),
                me: ConvertToScore(round.me)))
            .Select(round => (
                myScore: round.me,
                isDraw: round.oponent == round.me,
                victoryForMe: IsVictoryForA(round.me, round.oponent),
                victoryForOponent: IsVictoryForA(round.oponent, round.me)))
            .Select(round => (
                myScore: round.myScore,
                winScore: WinScore(round)))
            .Select(round => round.myScore + round.winScore)
            .Sum();

        Console.WriteLine($"Puzzle solution: {solution}");
    }
}
