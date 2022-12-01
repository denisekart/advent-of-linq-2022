using System.CommandLine;
using static AocTool;

var getSessionOption = new Option<bool?>(new[] { "--get", "-g" }, "Gets the current session information (reads the 'session' file in the repository root)");
var setSessionOption = new Option<string?>(new[] { "--set", "-s" }, "Sets the current session token. Session data can be obtained by copying the 'session' cookie value from the https://adventofcode.com/ site. Make sure you're logged in!");
var sessionCommand = new Command("session", "Gets or sets the authorization session used for downloading your personalized data from https://adventofcode.com/");
sessionCommand.AddOption(getSessionOption);
sessionCommand.AddOption(setSessionOption);
sessionCommand.SetHandler((getSession, setSession) =>
{
    if (getSession == true)
    {
        PrintSessionInformation();
        return;
    }
    if (!string.IsNullOrWhiteSpace(setSession))
    {
        SetSessionInformation(setSession);
    }

}, getSessionOption, setSessionOption);

var dayOption = new Option<string?>(new[] { "--day", "-d" }, "Specify the day of month (e.g. 1, day1) or leave blank for current day.");
var loadCommand = new Command("load", "Loads the data for the specified day (or today if the day was not specified).");
loadCommand.AddOption(dayOption);
loadCommand.SetHandler(async (string? day) =>
{
    await LoadDataForDay(day);

}, dayOption);

var generateCommand = new Command("generate", "Generates the test class and the cases fot the specified day.");
generateCommand.AddAlias("gen");
generateCommand.AddOption(dayOption);
generateCommand.SetHandler(async day =>
{
    ParseDayInformation(day, out var _, out var dayOfMonth, out var isValidDay);
    if (!isValidDay)
    {
        Console.Error.WriteLine($"The provided day is not valid. Expected a day between 1 and 25, got {dayOfMonth}. (Fun fact, you can only implicitly determine the day between 1st and 25th in a month)");
        throw new ArgumentOutOfRangeException();
    }
    await GenerateTestClass(dayOfMonth);
}, dayOption);

var rootCommand = new RootCommand("Advent Of Code tool");
rootCommand.AddCommand(sessionCommand);
rootCommand.AddCommand(loadCommand);
rootCommand.AddCommand(generateCommand);

await rootCommand.InvokeAsync(args);
