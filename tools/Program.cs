using System.CommandLine;
using System.Diagnostics;
using static AocTool;

var getSessionOption = new Option<bool?>(new[] { "--get", "-g" }, "Gets the current session information (reads the 'session' file in the repository root)");
var setSessionOption = new Option<string?>(new[] { "--set", "-s" }, "Sets the current session token. Session data can be obtained by copying the 'session' cookie value from the https://adventofcode.com/ site. Make sure you're logged in!");
var sessionCommand = new Command("session", "Gets or sets the authorization session used for downloading your personalized data from https://adventofcode.com/");
sessionCommand.AddOption(getSessionOption);
sessionCommand.AddOption(setSessionOption);
sessionCommand.SetHandler((getSession, setSession) =>
{
    Action action;
    if (getSession == true)
    {
        action = () => PrintSessionInformation();
    }
    else if (!string.IsNullOrWhiteSpace(setSession))
    {
        action = () => SetSessionInformation(setSession);
    }
    else
    {
        action = () => ConsoleWrappers.FailAndExit("Invalid input. Provide a --get or --set <token> option");
    }
    action.AndHandleErrors(successMessage: "Wow, you're a hacker now!");
}, getSessionOption, setSessionOption);

var dayOption = new Option<string?>(new[] { "--day", "-d" }, "Specify the day of month (e.g. 1, day1) or leave blank for current day.");
var loadCommand = new Command("load", "Loads the data for the specified day (or today if the day was not specified).");
loadCommand.AddOption(dayOption);
loadCommand.SetHandler(async (string? day) =>
{
    await LoadDataForDay(day).AndHandleErrors(successMessage: "Your data awaits m' lord!");

}, dayOption);

var generateCommand = new Command("generate", "Generates the test class and the cases fot the specified day.");
generateCommand.AddAlias("gen");
generateCommand.AddOption(dayOption);
generateCommand.SetHandler(async day =>
{
    ParseDayInformation(day, out var _, out var dayOfMonth, out var isValidDay);
    if (!isValidDay)
    {
        ConsoleWrappers.FailAndExit($"The provided day is not valid. Expected a day between 1 and 25, got {dayOfMonth}. (Fun fact, you can only implicitly determine the day between 1st and 25th in a month)");
    }
    await GenerateTestClass(dayOfMonth).AndHandleErrors(successMessage: "Now start coding ya' thin-leaved deciduous hardwood tree of the genus Betula!");
}, dayOption);

var makeMyDayCommand = new Command("makemyday", "Invokes generate and load commands and brings some christmas cheer");
makeMyDayCommand.AddAlias("d");
makeMyDayCommand.AddOption(dayOption);
makeMyDayCommand.SetHandler(async day =>
{
    ParseDayInformation(day, out var isDaySpecified, out var dayOfMonth, out var isValidDay);
    if (!isValidDay)
    {
        ConsoleWrappers.FailAndExit($"The provided day is not valid. Expected a day between 1 and 25, got {dayOfMonth}. (Fun fact, you can only implicitly determine the day between 1st and 25th in a month)");
    }

    await LoadDataForDay(day).AndTreatErrorsAsWarnings(successMessage: $"Data for day {dayOfMonth} downloaded");
    await GenerateTestClass(dayOfMonth).AndTreatErrorsAsWarnings(successMessage: $"Test class for day {dayOfMonth} generated");
    await GenerateBenchmarkClass(dayOfMonth).AndTreatErrorsAsWarnings(successMessage: $"Benchmark suite for day {dayOfMonth} generated");
    $"Now go to https://adventofcode.com/2022/day/{dayOfMonth} and start reading!".GoodDeveloper();

    ConsoleWrappers.SucceedAndExit("All is good in the world");
}, dayOption);

var updateReadmeOption = new Option<bool>(new[] { "--update", "-u" }, "Updates the README.md file with benchmark results");
var benchmarkCommand = new Command("benchmark", "Benchmarks everything");
benchmarkCommand.AddAlias("b");
benchmarkCommand.AddOption(updateReadmeOption);
benchmarkCommand.SetHandler(updateReadme =>
{
    ConsoleWrappers.SketchyDeveloper("This may take a while, sit back and relax(building stuff)...");

    CleanBenchmarkResults();
    CliRunner.RunDotNet("build -c Release");

    ConsoleWrappers.SketchyDeveloper("Okay, here comes the tricky part(running benchmarks)...");

    if (!CliRunner.RunDotNet("run --project .\\src\\Benchmarks\\Benchmarks.csproj -c Release"))
    {
        ConsoleWrappers.FailAndExit("Failed to run benchmarks");
    }
    if (updateReadme)
    {
        Console.WriteLine("Updating README.md with benchmark results");
        var benchmarks = GetSanitizedBenchmarkResults();
        var newReadme = GetUpdatedReadmeContent(benchmarks);
        UpdateReadmeWithNewContent(newReadme);
    }
    ConsoleWrappers.SucceedAndExit("All good! Have an amazing day. Oh...and commit the README.md please.");
}, updateReadmeOption);


var rootCommand = new RootCommand("Advent Of Code tool");
rootCommand.AddCommand(sessionCommand);
rootCommand.AddCommand(loadCommand);
rootCommand.AddCommand(generateCommand);
rootCommand.AddCommand(makeMyDayCommand);
rootCommand.AddCommand(benchmarkCommand);

await rootCommand.InvokeAsync(args);

public static class CliRunner
{
    public static bool RunDotNet(string command, string? cwd = null)
    {
        using var proc = Process.Start(new ProcessStartInfo
        {
            WorkingDirectory = cwd ?? AocTool.FindThisGitRepositoryRoot()!.FullName,
            FileName = "dotnet",
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,

        });

        if (proc == null)
        {
            return false;
        }

        while (!proc.StandardOutput.EndOfStream)
        {
            var line = proc.StandardOutput.ReadLine();
            Console.WriteLine(line);
        }

        proc.StandardError.ReadToEnd().BadDeveloper();

        proc.WaitForExit();
        return proc.ExitCode == 0;
    }
}